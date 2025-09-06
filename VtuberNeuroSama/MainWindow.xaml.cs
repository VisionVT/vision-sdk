using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Spout2; // Spout2 integration

namespace AriaSama
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly SpeechSynthesizer _tts = new SpeechSynthesizer();
        private readonly List<string> _context = new List<string>();
        private readonly HashSet<string> _bannedWords = new HashSet<string> { "badword1", "badword2" };
        private SpeechRecognitionEngine _recognizer;
        private bool _isListening = false;
        private SpoutSender _spoutSender;
        private string _currentAvatarState = "idle";

        public MainWindow()
        {
            InitializeComponent();
            SendButton.Click += SendButton_Click;
            ChatInput.KeyDown += (s, e) => { if (e.Key == System.Windows.Input.Key.Enter) SendButton_Click(s, e); };
            MicButton.Click += MicButton_Click;
            LoadAvatar();
            InitSpeechRecognition();
            _spoutSender = new SpoutSender("AriaSamaAvatar");
        }

        private void LoadAvatar(string state = "idle")
        {
            string fileName = state switch
            {
                "happy" => "avatar_happy.png",
                "sad" => "avatar_sad.png",
                "talking" => "avatar_talking.png",
                _ => "avatar.png"
            };
            try
            {
                var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                AvatarImage.Source = new BitmapImage(new Uri(path));
                SendAvatarToSpout();
            }
            catch { }
        }

        private void SendAvatarToSpout()
        {
            if (AvatarImage.Source is BitmapSource bmp)
                _spoutSender.SendBitmapSource(bmp);
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            await ProcessUserMessage(ChatInput.Text.Trim());
        }

        private async Task ProcessUserMessage(string userMessage)
        {
            if (string.IsNullOrEmpty(userMessage)) return;
            if (ContainsBannedWord(userMessage))
            {
                ChatHistory.Items.Add($"[MOD] Message blocked.");
                ChatInput.Clear();
                return;
            }
            ChatHistory.Items.Add($"You: {userMessage}");
            _context.Add($"User: {userMessage}");
            ChatInput.Clear();
            var aiReply = await GetOllamaResponse(string.Join("\n", _context));
            ChatHistory.Items.Add($"Aria-Sama: {aiReply}");
            _context.Add($"Aria-Sama: {aiReply}");
            _tts.SpeakAsync(aiReply);
            // Animation logic based on AI reply
            if (aiReply.Contains("smile") || aiReply.Contains("happy"))
                SetAvatarState("happy");
            else if (aiReply.Contains("sad"))
                SetAvatarState("sad");
            else if (aiReply.Contains("talk"))
                SetAvatarState("talking");
            else
                SetAvatarState("idle");
        }

        private void SetAvatarState(string state)
        {
            _currentAvatarState = state;
            LoadAvatar(state);
        }

        private bool ContainsBannedWord(string message)
        {
            var lower = message.ToLower();
            return _bannedWords.Any(bw => lower.Contains(bw));
        }

        private async Task<string> GetOllamaResponse(string prompt)
        {
            var requestBody = new { model = "llama2", prompt = prompt };
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            try
            {
                var response = await _httpClient.PostAsync("http://localhost:11434/api/generate", content);
                var responseString = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(responseString);
                if (doc.RootElement.TryGetProperty("response", out var reply))
                    return reply.GetString();
                return "[Error: No response from Ollama]";
            }
            catch (Exception ex)
            {
                return $"[Error: {ex.Message}]";
            }
        }

        // --- Voice Input ---
        private void InitSpeechRecognition()
        {
            try
            {
                _recognizer = new SpeechRecognitionEngine();
                _recognizer.SetInputToDefaultAudioDevice();
                _recognizer.LoadGrammar(new DictationGrammar());
                _recognizer.SpeechRecognized += (s, e) => {
                    Dispatcher.Invoke(async () => {
                        await ProcessUserMessage(e.Result.Text);
                    });
                };
                _recognizer.RecognizeCompleted += (s, e) => { _isListening = false; MicButton.Content = "??"; };
            }
            catch { }
        }

        private void MicButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isListening)
            {
                _recognizer.RecognizeAsyncStop();
                _isListening = false;
                MicButton.Content = "??";
            }
            else
            {
                _recognizer.RecognizeAsync(RecognizeMode.Multiple);
                _isListening = true;
                MicButton.Content = "?";
            }
        }

        // --- Spout2 Capture Placeholder ---
        // TODO: Integrate Spout2 to share avatar area as a video texture
        // Example: SpoutSender sender = new SpoutSender("AriaSamaAvatar"); sender.SendBitmapSource(AvatarImage.Source as BitmapSource);
        // This requires Spout2 .NET library and proper setup
    }
}

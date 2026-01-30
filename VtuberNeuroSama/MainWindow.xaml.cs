using System.Net.Http;
using System.Windows;
using System.Windows.Input;

namespace AriaSama
{
    public partial class MainWindow : Window
    {
        private HttpClient _httpClient = new();
        private string _ollamaUrl = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void FileMenu_Click(object sender, RoutedEventArgs e) => AppendMessage("[MENU] File clicked");
        private void EditMenu_Click(object sender, RoutedEventArgs e) => AppendMessage("[MENU] Edit clicked");
        private void ViewMenu_Click(object sender, RoutedEventArgs e) => AppendMessage("[MENU] View clicked");
        private void ToolsMenu_Click(object sender, RoutedEventArgs e) => AppendMessage("[MENU] Tools clicked");
        private void HelpMenu_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Aria-Sama v0.1.0\nVTuber AI Platform", "About");

        private void VTuberModels_Click(object sender, RoutedEventArgs e) => AppendMessage("[ACTION] VTuber Models");
        private void Chat_Click(object sender, RoutedEventArgs e) => AppendMessage("[ACTION] Chat selected");
        private void Settings_Click(object sender, RoutedEventArgs e) => AppendMessage("[ACTION] Settings clicked");

        private async void DiscoverOllama_Click(object sender, RoutedEventArgs e)
        {
            AppendMessage("[INFO] Scanning network for Ollama...");
            bool found = false;

            string[] baseIPs = { "192.168.1.", "192.168.0.", "10.0.0." };

            foreach (var baseIP in baseIPs)
            {
                for (int i = 1; i <= 254; i++)
                {
                    string ip = $"{baseIP}{i}";
                    try
                    {
                        var testUrl = $"http://{ip}:11434/api/tags";
                        using (var cts = new System.Threading.CancellationTokenSource(500))
                        {
                            var response = await _httpClient.GetAsync(testUrl, HttpCompletionOption.ResponseHeadersRead, cts.Token);
                            if (response.IsSuccessStatusCode)
                            {
                                _ollamaUrl = $"http://{ip}:11434";
                                OllamaIpInput.Text = ip;
                                ConnectionStatus.Text = "?? Connected";
                                OllamaStatus.Text = "?? Connected";
                                AppendMessage($"[SUCCESS] Found Ollama at {_ollamaUrl}");
                                found = true;
                                break;
                            }
                        }
                    }
                    catch { }
                }
                if (found) break;
            }

            if (!found)
            {
                AppendMessage("[ERROR] Ollama not found. Enter IP manually.");
            }
        }

        private void ChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Send_Click(null, null);
                e.Handled = true;
            }
        }

        private async void Send_Click(object? sender, RoutedEventArgs? e)
        {
            string message = ChatInput.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(message) || message == "Type message...") return;

            AppendMessage($"You: {message}");
            ChatInput.Clear();

            if (string.IsNullOrEmpty(_ollamaUrl))
            {
                string ip = OllamaIpInput.Text?.Trim() ?? "";
                if (!string.IsNullOrEmpty(ip))
                {
                    _ollamaUrl = $"http://{ip}:11434";
                }
                else
                {
                    AppendMessage("[ERROR] Set Ollama IP first");
                    return;
                }
            }

            try
            {
                var model = ModelInput.Text?.Trim() ?? "llama2";
                AppendMessage("[INFO] Calling Ollama...");

                var requestBody = new { model = model, prompt = message, stream = false };
                var jsonContent = System.Text.Json.JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                using (var cts = new System.Threading.CancellationTokenSource(30000))
                {
                    var response = await _httpClient.PostAsync($"{_ollamaUrl}/api/generate", content, cts.Token);
                    var responseText = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        using var doc = System.Text.Json.JsonDocument.Parse(responseText);
                        if (doc.RootElement.TryGetProperty("response", out var reply))
                        {
                            string replyText = reply.GetString() ?? "[Empty]";
                            AppendMessage($"Aria-Sama: {replyText}");
                        }
                    }
                    else
                    {
                        AppendMessage($"[ERROR] {response.StatusCode}");
                    }
                }
            }
            catch (System.Threading.Tasks.TaskCanceledException)
            {
                AppendMessage("[ERROR] Timeout - check Ollama connection");
            }
            catch (Exception ex)
            {
                AppendMessage($"[ERROR] {ex.Message}");
            }
        }

        private void AppendMessage(string msg)
        {
            ChatMessages.Text += msg + "\n";
        }
    }
}



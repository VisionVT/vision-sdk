# Aria-Sama | VTuber AI Platform

**Status: Early Alpha - Functional prototype with Ollama integration**

## Overview
Aria-Sama is a modern VTuber AI Assistant platform built with .NET 8.0 WPF. It features a Visual Studio 2026-inspired UI and integrates with local Ollama instances for AI-powered conversations.

## Latest Features (v0.1.0)

### ? Completed
- **VS 2026 Inspired GUI** - Modern dark theme with professional styling
- **Real-time Chat Interface** - Send and receive messages with Ollama
- **Ollama Auto-Discovery** - Automatically scan local network for Ollama instances (192.168.1.x, 192.168.0.x, 10.0.0.x)
- **Manual Ollama Configuration** - Set custom IP and model selection
- **Connection Status Indicator** - Live connection status (?? Connected / ?? Disconnected)
- **Functional Menu Bar** - File, Edit, View, Tools, Help (with click handlers)
- **Sidebar Navigation** - VTuber Models, Chat, Settings
- **Properties Panel** - Display connection info, IP configuration, model selection
- **Keyboard Support** - Press Enter to send messages

### ?? In Progress
- VTuber model upload interface
- Voice synthesis integration
- Stream overlay support

## Platform
- **OS:** Windows NT (Windows 10/11 required)
- **Framework:** .NET 8.0 WPF
- **AI Backend:** Local Ollama instances

## Setup Instructions

### Prerequisites
- Windows 10 or 11
- .NET 8.0 SDK ([download](https://dotnet.microsoft.com/en-us/download/dotnet/8.0))
- Visual Studio 2022/2026 (or Visual Studio Code with C# extension)
- Ollama (optional but required for AI features) - [download Ollama](https://ollama.ai)

### Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/VisionVT/vision-sdk.git
   cd vision-sdk
   ```

2. Restore dependencies:
   ```sh
   dotnet restore
   ```

### Running the Application

#### Option 1: Visual Studio (Recommended)
1. Open `vision-sdk.sln` in Visual Studio
2. Right-click `VtuberNeuroSama` project ? Set as Startup Project
3. Press `F5` or go to Debug ? Start Debugging

#### Option 2: Command Line
```sh
dotnet build VtuberNeuroSama\VtuberNeuroSama.csproj
dotnet run --project VtuberNeuroSama\VtuberNeuroSama.csproj
```

## How to Use Aria-Sama

### 1. Launch the Application
- Run from Visual Studio (F5) or execute the .exe directly
- Window opens with title: "Aria-Sama | VTuber AI Platform"

### 2. Connect to Ollama

**Automatic Discovery:**
- Click **"?? Discover"** button in the left sidebar
- App scans your local network for Ollama instances
- If found, connection status updates to "?? Connected"

**Manual Configuration:**
- In the Properties panel (right side), enter your Ollama server IP
- Update the Model field if needed (default: `llama2`)
- Connection will be established when you send your first message

### 3. Chat with Aria-Sama
1. Type your message in the input field at the bottom
2. Press **Enter** or click **Send**
3. Message appears in the chat display
4. Aria-Sama queries your Ollama instance and responds
5. Response appears in real-time

### 4. Features
- **Menu Bar:** File, Edit, View, Tools, Help (expandable for future features)
- **Sidebar:** Quick access to VTuber Models, Chat, Settings
- **Properties Panel:** Monitor connection status, configure Ollama
- **Chat History:** All messages logged in the main chat area

## Troubleshooting

### App Won't Launch
- Ensure .NET 8.0 is installed: `dotnet --version`
- Try rebuilding: `dotnet clean` then `dotnet build`

### Can't Connect to Ollama
- Verify Ollama is running: `ollama serve` (in a separate terminal)
- Check Ollama is listening on port 11434
- Try entering IP manually instead of auto-discovery
- Ensure firewall allows connections to Ollama port

### "No Ollama instance found"
- Make sure Ollama server is running on your network
- Try manual IP entry in the Properties panel
- Verify IP address and port (default: 11434)

## Project Structure
```
vision-sdk/
??? VtuberNeuroSama/
?   ??? Program.cs              # WPF Application entry point
?   ??? App.xaml                # Application resources and styles
?   ??? App.xaml.cs             # Application code-behind
?   ??? MainWindow.xaml         # Main UI layout
?   ??? MainWindow.xaml.cs      # UI logic and Ollama integration
?   ??? VtuberNeuroSama.csproj  # Project configuration
??? README.md                   # This file
```

## Architecture

### Key Components
- **MainWindow.xaml** - UI layout with chat interface, sidebar, and properties panel
- **MainWindow.xaml.cs** - Event handlers, Ollama discovery, and API integration
- **App.xaml** - Global resources (colors, fonts, button styles)
- **Program.cs** - WPF application startup

### Ollama Integration
- Uses `HttpClient` to communicate with Ollama API
- Endpoint: `http://{ip}:11434/api/generate`
- Supports streaming and non-streaming responses
- Timeout: 30 seconds per request

## Development Notes

### Building
```sh
dotnet build VtuberNeuroSama\VtuberNeuroSama.csproj
```

### Running Tests
Currently no automated tests. Manual testing recommended.

### Code Style
- C# 11 features (nullable reference types, implicit usings)
- WPF XAML following Microsoft guidelines
- VS 2026 color palette for UI consistency

## Future Roadmap

- [ ] VTuber model uploader with preview
- [ ] Voice synthesis (text-to-speech)
- [ ] Streamlabs Desktop integration
- [ ] Chat history persistence
- [ ] Model switching during conversation
- [ ] Response formatting options
- [ ] Multi-language support
- [ ] Custom theme support

## Known Limitations

- Single Ollama connection (doesn't support multiple instances)
- Chat history not persisted between sessions
- No response interrupt/cancellation
- Limited error recovery
- No proxy support

## Contributing
Contributions are welcome! Please ensure:
- Code follows existing style conventions
- Changes build successfully
- New features include basic error handling

## License
[Add your license here]

## Contact & Support
- **Repository:** https://github.com/VisionVT/vision-sdk
- **Issues:** Report bugs on GitHub Issues
- **Email:** [Add contact email]

---
**Aria-Sama v0.1.0** | VisionVT | 2025

**Built with:**
- .NET 8.0 WPF
- Modern UI/UX design
- Local Ollama AI integration

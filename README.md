# Vision SDK (IN ALPHA) v0.05

An open-source AI VTuber companion application inspired by Vision SDK, featuring real-time interactions, screen vision, and seamless integrations.

## Features

### Core Functionality
- **AI Chat Interface**: Engage in natural conversations with AI using OpenAI GPT or Ollama models
- **Text-to-Speech (TTS)**: Toggle voice output for AI responses using browser-based speech synthesis
- **Twitch Integration**: Real-time chat monitoring and interaction with Twitch channels
- **Screen Vision**: Capture and analyze your computer screen for AI-powered insights

### User Interface
- **Floating VTuber Model**: Customizable character avatar positioned on-screen with transparent background
- **Vision SDK-Inspired Design**: Dark theme UI with modern, VTuber-style aesthetics
- **Chat Window**: Dedicated interface for AI conversations with message history
- **Vision Controls**: Screen sharing and screenshot capture tools

### Technical Features
- **Web-Based Architecture**: Runs in modern browsers with real-time updates via Socket.IO
- **Configuration Wizard**: Branded setup process for API keys and preferences
- **Multi-Model Support**: Fallback between OpenAI and local Ollama instances
- **Real-Time Communication**: Live updates for chat, vision, and integrations

### Integrations
- **OpenAI API**: Primary AI processing with GPT models
- **Ollama**: Local AI model support for offline capabilities
- **Twitch IRC**: Direct chat integration using tmi.js
- **WebRTC**: Screen capture and media streaming

## Architecture

The application consists of:
- **Frontend**: HTML/CSS/JavaScript interface with Socket.IO client
- **Backend**: Node.js Express server handling AI requests and integrations
- **Storage**: Local configuration and session management

## Project Structure

```
vision-sdk/
├── server.js              # Main server application
├── package.json           # Node.js dependencies
├── public/
│   ├── index.html         # Main VTuber interface
│   └── setup.html         # Configuration wizard
├── config.json            # API configuration (generated)
└── README.md              # This file
```

## Technologies Used

- **Frontend**: HTML5, CSS3, JavaScript (ES6+)
- **Backend**: Node.js, Express.js
- **Real-Time**: Socket.IO
- **AI**: OpenAI API, Ollama
- **Twitch**: tmi.js library
- **HTTP Client**: Axios

## Vision

Vision SDK aims to provide a comprehensive platform for AI VTuber development, combining computer vision, natural language processing, and real-time social media integration into a user-friendly companion application.

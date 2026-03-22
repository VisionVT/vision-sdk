# Vision SDK Web Integration

This adds Twitch chat integration and screen vision support through a Vision SDK web interface.

## Setup

1. Install Node.js (https://nodejs.org/) - download and install the LTS version
2. Run `npm install`
3. Run `npm start`
4. Open http://localhost:3000/setup to configure API keys
5. Enter your OpenAI API key and Ollama URL
6. Access the main interface at http://localhost:3000

## Features

- Vision SDK branded setup wizard
- Vision SDK-inspired UI with character avatar
- Floating VTuber model on the side (transparent background)
- AI chat window with text-to-speech (TTS) toggle
- Real-time Twitch chat display
- Screen capture and screenshot functionality (vision support)
- Socket.IO for real-time communication

## Notes

- Screen sharing requires user permission
- TTS uses browser's speech synthesis
- AI chat uses OpenAI API (with Ollama fallback)
- Twitch integration uses tmi.js library
- UI styled with a dark theme similar to AIRI
- Configuration saved locally and on server
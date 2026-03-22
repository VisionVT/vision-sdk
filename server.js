const express = require('express');
const http = require('http');
const socketIo = require('socket.io');
const tmi = require('tmi.js');
const fs = require('fs');
const axios = require('axios');

const app = express();
const server = http.createServer(app);
const io = socketIo(server);

// Twitch bot setup
const twitchClient = new tmi.Client({
  options: { debug: true },
  connection: {
    secure: true,
    reconnect: true
  },
  identity: {
    username: process.env.TWITCH_USERNAME || 'your_bot_username',
    password: process.env.TWITCH_OAUTH || 'oauth:your_oauth_token'
  },
  channels: [process.env.TWITCH_CHANNEL || 'your_channel']
});

twitchClient.connect().catch(console.error);

twitchClient.on('message', (channel, tags, message, self) => {
  if (self) return;
  io.emit('twitchMessage', {
    username: tags['display-name'],
    message: message,
    color: tags.color
  });
});

app.use(express.static('public'));

app.get('/', (req, res) => {
  res.sendFile(__dirname + '/public/index.html');
});

app.get('/setup', (req, res) => {
  res.sendFile(__dirname + '/public/setup.html');
});

io.on('connection', (socket) => {
  console.log('User connected');

  socket.on('saveConfig', (config) => {
    // Save to a config file or environment
    const configData = {
      openaiKey: config.openaiKey,
      ollamaUrl: config.ollamaUrl
    };
    fs.writeFileSync('config.json', JSON.stringify(configData, null, 2));
    console.log('Config saved:', configData);
  });

  socket.on('aiMessage', async (data) => {
    try {
      // Try OpenAI first
      const response = await axios.post('https://api.openai.com/v1/chat/completions', {
        model: 'gpt-3.5-turbo',
        messages: [{ role: 'user', content: data.message }]
      }, {
        headers: {
          'Authorization': `Bearer ${data.openaiKey}`,
          'Content-Type': 'application/json'
        }
      });
      const aiResponse = response.data.choices[0].message.content;
      socket.emit('aiResponse', { response: aiResponse });
    } catch (error) {
      console.error('OpenAI error:', error.message);
      // Fallback to Ollama
      try {
        const ollamaResponse = await axios.post(`${data.ollamaUrl}/api/generate`, {
          model: 'llama2',
          prompt: data.message,
          stream: false
        });
        socket.emit('aiResponse', { response: ollamaResponse.data.response });
      } catch (ollamaError) {
        console.error('Ollama error:', ollamaError.message);
        socket.emit('aiResponse', { response: 'Sorry, I couldn\'t process your message.' });
      }
    }
  });

  socket.on('disconnect', () => {
    console.log('User disconnected');
  });
});

const PORT = process.env.PORT || 3000;
server.listen(PORT, () => {
  console.log(`Vision SDK server running on port ${PORT}`);
});
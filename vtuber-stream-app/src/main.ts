import React from 'react';
import ReactDOM from 'react-dom';
import VtuberModelUploader from './components/VtuberModelUploader';
import VoiceChat from './components/VoiceChat';
import StreamlabsIntegration from './components/StreamlabsIntegration';

const App = () => {
    return (
        <div>
            <h1>Vtuber Stream Application</h1>
            <VtuberModelUploader />
            <VoiceChat />
            <StreamlabsIntegration />
        </div>
    );
};

ReactDOM.render(<App />, document.getElementById('root'));
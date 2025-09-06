import React, { useEffect, useRef, useState } from 'react';

const VoiceChat: React.FC = () => {
    const [isRecording, setIsRecording] = useState(false);
    const [mediaRecorder, setMediaRecorder] = useState<MediaRecorder | null>(null);
    const [audioChunks, setAudioChunks] = useState<Blob[]>([]);
    const audioRef = useRef<HTMLAudioElement | null>(null);

    useEffect(() => {
        if (mediaRecorder) {
            mediaRecorder.ondataavailable = (event) => {
                setAudioChunks((prev) => [...prev, event.data]);
            };

            mediaRecorder.onstop = () => {
                const audioBlob = new Blob(audioChunks, { type: 'audio/webm' });
                const audioUrl = URL.createObjectURL(audioBlob);
                if (audioRef.current) {
                    audioRef.current.src = audioUrl;
                }
                setAudioChunks([]);
            };
        }
    }, [mediaRecorder, audioChunks]);

    const startRecording = async () => {
        const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
        const recorder = new MediaRecorder(stream);
        setMediaRecorder(recorder);
        recorder.start();
        setIsRecording(true);
    };

    const stopRecording = () => {
        if (mediaRecorder) {
            mediaRecorder.stop();
            setIsRecording(false);
        }
    };

    return (
        <div>
            <h2>Voice Chat</h2>
            <button onClick={isRecording ? stopRecording : startRecording}>
                {isRecording ? 'Stop Recording' : 'Start Recording'}
            </button>
            <audio ref={audioRef} controls />
        </div>
    );
};

export default VoiceChat;
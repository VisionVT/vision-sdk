import React, { useState } from 'react';

const VtuberModelUploader: React.FC = () => {
    const [modelFile, setModelFile] = useState<File | null>(null);
    const [error, setError] = useState<string | null>(null);

    const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0];
        if (file) {
            const validExtensions = ['.vrm', '.fbx', '.glb'];
            const fileExtension = file.name.slice((file.name.lastIndexOf(".") - 1 >>> 0) + 2);
            if (validExtensions.includes(`.${fileExtension}`)) {
                setModelFile(file);
                setError(null);
            } else {
                setError('Invalid file type. Please upload a .vrm, .fbx, or .glb file.');
            }
        }
    };

    const handleUpload = () => {
        if (modelFile) {
            // Logic to upload the model file
            console.log('Uploading model:', modelFile.name);
            // Reset the file input after upload
            setModelFile(null);
        }
    };

    return (
        <div>
            <h2>Upload Vtuber Model</h2>
            <input type="file" accept=".vrm,.fbx,.glb" onChange={handleFileChange} />
            {error && <p style={{ color: 'red' }}>{error}</p>}
            <button onClick={handleUpload} disabled={!modelFile}>Upload</button>
        </div>
    );
};

export default VtuberModelUploader;
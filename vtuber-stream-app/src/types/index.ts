export interface VtuberModel {
    id: string;
    name: string;
    filePath: string;
    uploadDate: Date;
}

export interface VoiceChatSettings {
    isEnabled: boolean;
    volume: number;
    inputDeviceId: string;
    outputDeviceId: string;
}

export interface StreamlabsIntegrationSettings {
    isConnected: boolean;
    streamKey: string;
    vtuberModelId: string;
}
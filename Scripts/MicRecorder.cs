using UnityEngine;

public class MicRecorder
{

    private AudioClip _audioClip;
    private string _micDevice;

    public bool StartRecording(int microphoneIndex = 0)
    {
        if (Microphone.devices[microphoneIndex] == null) return false;
        _micDevice = Microphone.devices[microphoneIndex];
        
        _audioClip = Microphone.Start(null, false, 5, 16000);
        Debug.Log("Starting recording");
        return true;
    }

    public void StopRecording()
    {
        if (Microphone.IsRecording(_micDevice))
        {
            Microphone.End(_micDevice);
            Debug.Log("Stopping recording");
        }
    }

    public AudioClip GetAudioClip()
    {
        return _audioClip;
    }

}

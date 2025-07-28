using System;
using StringMetric;
using UnityEngine;
using UnityEngine.InputSystem;
using VoiceService;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; } 

    private string _whisperBinary = Application.streamingAssetsPath + "/mac/whisper-cli";
    private string _whisperModel = Application.streamingAssetsPath + "/ggml-large-v3-turbo-q5_0.bin";

    public event Action<string> OnTryToCast;
    
    private MicRecorder _micRecorder = new MicRecorder();
    

    private IVoiceService _voiceService;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        _voiceService = new WhisperVoiceService(_whisperBinary, _whisperModel);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputSystem.actions["CastSpell"].WasPressedThisFrame())
        {
            bool success = _micRecorder.StartRecording();
            if (!success)
            {
                Debug.Log("Failed to start recording");
            }
        }
        if (InputSystem.actions["CastSpell"].WasReleasedThisFrame())
        {
            _micRecorder.StopRecording();
            SavWav.Save(Application.streamingAssetsPath + "/latest-spell.wav", _micRecorder.GetAudioClip());
            string transcript = _voiceService.GetTranscript(Application.streamingAssetsPath + "/latest-spell.wav");
            if (transcript == null) return;
            transcript.TrimStart(' '); // trims cause sometimes start with an empty char
            IStringMetric stringMetric = new LevenshteinDistance();
            int range = stringMetric.CalculateDistance("Knight.", transcript);
            if (range <= 3)
            {
                Debug.Log("Is in range of \"knight\": " + range);
            }
            else
            {
                Debug.Log("Is not in range of \"knight\": " + range);
            }

            OnTryToCast?.Invoke(transcript);
        }
            
    }
}

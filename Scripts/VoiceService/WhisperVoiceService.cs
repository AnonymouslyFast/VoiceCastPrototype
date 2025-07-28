using System;
using UnityEngine;
using System.IO;
using System.Threading;
using Whisper;

namespace VoiceService
{
    public class WhisperVoiceService : IVoiceService
    {
        
        private WhisperWrapper _whisperWrapper;

        public WhisperVoiceService(string binaryPath, string modelPath)
        {
            _whisperWrapper = new WhisperWrapper(binaryPath, modelPath);
        }

        String IVoiceService.GetTranscript(string wavPath)
        {
            String transcript = null;
            string path = _whisperWrapper.Transcribe(wavPath);
            
            if (File.Exists(path))
            {   
                transcript = File.ReadAllText(path);
            }
            return transcript;
        }
    }
}

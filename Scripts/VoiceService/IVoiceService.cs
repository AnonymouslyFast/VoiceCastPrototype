using System;
using UnityEngine;

namespace VoiceService
{
    public interface IVoiceService
    {
        String GetTranscript(string wavPath);
    }
}


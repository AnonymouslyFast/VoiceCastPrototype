using System;
using System.IO;
using System.Diagnostics;
using UnityEngine;

namespace Whisper
{
    public class WhisperWrapper
    {

        private string binaryPath;
        private string modelPath;
        
        public WhisperWrapper(string binaryPath, string modelPath)
        {
            this.binaryPath = binaryPath;
            this.modelPath = modelPath;
        }

        // Transcribes given .wav file to a txt file then returns the contents of the txt file
        public string Transcribe(string wavPath)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = binaryPath,
                Arguments = $"-m \"{modelPath}\" -f \"{wavPath}\" -otxt",
                UseShellExecute = false,
                RedirectStandardOutput = false,
                CreateNoWindow = true,
                WorkingDirectory = Application.streamingAssetsPath
            };
            
            var process = Process.Start(startInfo);
            process.WaitForExit();
            
            string txtPath = wavPath + ".txt"; 
            return txtPath;
        }
    }
}
using System;
using StringMetric;
using UnityEngine;

public class LevenshteinDistance : IStringMetric
{
    int IStringMetric.CalculateDistance(string baseString, string inputString)
    {
        
        int [,] wordMatrix = new int[baseString.Length + 1, inputString.Length + 1];
        
        /* 
         * Example of this matrix would be the words `cat` and `cot`
         * 4x4 matrix since both have lengths of 3 chars, and there's a blank space for system to calculate
         *      ø c a t
         *     +–––––––
         *   ø |0 1 2 3
         *   c |1
         *   o |2
         *   t |3
         */
        
        // Initializing first row and column based on the base and input string.
        for (int i = 0; i < baseString.Length; i++)
            wordMatrix[i, 0] = i; 
        
        for (int j = 0; j < inputString.Length; j++)
            wordMatrix[0, j] = j; 
        
        // Calculating Distance
        for (int i = 1; i <= baseString.Length; i++)
        {
            for (int j = 1; j <= inputString.Length; j++)
            {
                bool isSameCharacter = baseString[i - 1] == inputString[j - 1];
                int costOfSubstitution = isSameCharacter ? 0 : 1;
                
                int deletion = wordMatrix[i - 1, j] + 1;
                int insertion = wordMatrix[i, j - 1] + 1;
                int substitution = wordMatrix[i - 1, j - 1] + costOfSubstitution;
                
                wordMatrix[i, j] = Math.Min(Math.Min(deletion, insertion), substitution);
            }
        }

        Debug.Log(wordMatrix.ToString());
        return wordMatrix[baseString.Length, inputString.Length];
    }
}

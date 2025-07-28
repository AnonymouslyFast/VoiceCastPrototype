using UnityEngine;

namespace StringMetric
{
    public interface IStringMetric
    {
        int CalculateDistance(string baseString, string inputString);
    }
}

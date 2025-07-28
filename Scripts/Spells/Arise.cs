using UnityEngine;

public class Arise : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.OnTryToCast += OnTryToCast;
    }

    private void OnTryToCast(string eventChant) 
    {
        Debug.Log(eventChant);
        if (eventChant.StartsWith("Fuck"))
        {
            Debug.Log(eventChant);
        }
    }

}

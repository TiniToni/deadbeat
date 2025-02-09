using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player") && GameManager.Instance != null)
    {
        if (GameManager.Instance != null)
{
    GameManager.Instance.StartDangerSequence();
}
else
{
    Debug.LogError("GameManager instance is null! Cannot start danger sequence.");
}

    }
}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

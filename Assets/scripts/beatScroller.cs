using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beatScroller : MonoBehaviour
{
    public float beatTempo;
    public bool started;
    // Start is called before the first frame update
    void Start()
{
    beatTempo = beatTempo / 60f;

    // Ensure started flag is initialized
    if (!started)
    {
        Debug.Log("Beat Scroller not started.");
    }
}

void Update()
{
    if (started)
    {
        transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
    }
    else
    {
        Debug.Log("Beat scroller hasn't started yet.");
    }
}

}

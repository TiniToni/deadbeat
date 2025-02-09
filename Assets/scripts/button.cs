using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite image;
    public Sprite image2;

    public KeyCode keyToPressed;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void setUnpressed()
    {
        sr.sprite = image;
    }

    void setPressed()
    {
        sr.sprite = image2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPressed))
        {
            setPressed();
        }
        if (Input.GetKeyUp(keyToPressed))
        {
            setUnpressed();
        }
    }
}

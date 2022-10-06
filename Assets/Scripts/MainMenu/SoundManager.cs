using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public bool isMoving;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("Horizontal") || Input.GetKeyDown("Vertical"))
        {
            isMoving = true;
        }
        if (Input.GetKeyUp("Horizontal") || Input.GetKeyUp("Vertical"))
        {
            isMoving = false;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicTry : MonoBehaviour
{
    public SpriteRenderer orb;
    public GameObject orb2;
    //public float frequency = 5f;
    //public float magnitude = 5f;
    //public float offset = 0f;


    /*
    public void Update()
    {
        transform.position = transform.up * Mathf.Sin(Time.time * frequency + offset) * magnitude;
    }
   */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            orb2.SetActive(false);
        }
    }
}

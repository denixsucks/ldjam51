using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbCollecting : MonoBehaviour
{
    public GameObject orb;
    public bool isEnemyDead;
    public Transform playerPos;
    public Transform orbPos;
    public float orbSpeed;
    public AnimationCurve curve;
    [SerializeField] private float currentTime = 0f;
    [SerializeField] private float duration = 1f;

    

    private void Start()
    {
        currentTime = 0;
    }

    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isEnemyDead = true;
        }

        if (isEnemyDead)
        {
            CallBack();
        }
    }
    private void CallBack()
    {
        //transform.position = Vector2.MoveTowards(transform.position, playerPos.position, orbSpeed * Time.deltaTime);
        orbPos.position = Vector2.MoveTowards(orbPos.position, playerPos.position , Time.deltaTime * orbSpeed * curve.Evaluate(TimeManagement()));

    }


    //ins dogrudur
    private float TimeManagement()
    {

        currentTime += Time.deltaTime;
        return currentTime / duration; //0dan bire
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("orb"))
        {
            orb.SetActive(false);
        }
    }
}

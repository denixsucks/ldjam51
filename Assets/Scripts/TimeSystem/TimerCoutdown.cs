using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCoutdown : MonoBehaviour
{
    [Header("TimeStuffs")]
    public Player playerController;
    float currentTime;
    public float startingTime = 10f;
    public float levelLoadTime = 4f;

    public bool isLevelChanging;
    public Levels levelController;

    [SerializeField] Text countDownText;

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        if (isLevelChanging == true)
        {
            //levelController.levelChangeSystem();
        }

        currentTime -= Time.deltaTime;
        countDownText.text = currentTime.ToString("0.00");

        if (currentTime <= 0)
        {
            currentTime = 0;
            StartCoroutine("timeController");
        }
    }

    IEnumerator timeController()
    {
        isLevelChanging = true;
        playerController.activeMoveSpeed = 0f;
        yield return new WaitForSeconds(levelLoadTime);
        StartCoroutine("resetTimer");
        isLevelChanging = false;
        playerController.activeMoveSpeed = playerController.MovementSpeed;
        
    }

    void resetTimer()
    {
        currentTime = startingTime;
    }
}

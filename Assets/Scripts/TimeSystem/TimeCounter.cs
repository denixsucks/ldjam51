using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeCounter : MonoBehaviour
{
  public static TimeCounter Instance {get; private set;}
  const float timer = 10f;
  private float currentTime;
  public Text timerText;
  
  void Awake()
  {
    if(Instance != null && Instance != this) {
      Destroy(this);
    }
    else {
      Instance = this;
      resetTimer();
    }
  }
  void Update()
  { 
    if (currentTime <= 0) resetTimer();
    currentTime -= Time.deltaTime;
    timerText.text = currentTime.ToString("0.00");
  }
  void resetTimer() => currentTime = timer;
}

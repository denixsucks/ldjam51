using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeCounter : MonoBehaviour
{
  public static TimeCounter Instance {get; private set;}
  const float startTime = 10f;
  const float levelLoadTime = 4f;
  private float currentTime;
  private bool isLevelChanging = false;
  public Player player;
  public Levels levelChanger;
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
    if (currentTime <= 0 && !isLevelChanging) 
      StartCoroutine("levelChange");
    
    if (!isLevelChanging)
      currentTime -= Time.deltaTime;

    timerText.text = currentTime.ToString("0.00");
  }
  IEnumerator levelChange()
  {
    isLevelChanging = true;
    player.freezePlayer();
    yield return new WaitForSeconds(levelLoadTime - levelLoadTime/2f);
    levelChanger.teleportPlayerToArea();
    resetTimer();
    yield return new WaitForSeconds(levelLoadTime - levelLoadTime/2f);
    player.releasePlayer();
    isLevelChanging = false;
    yield break;
  }

  void resetTimer() => currentTime = startTime;
}

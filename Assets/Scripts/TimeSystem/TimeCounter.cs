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
  public PlayerMovement player;
  public Levels levelChanger;
  public Text timerText;
  public Animator anim;
  public Material mat;

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
    if (currentTime <= 0 && !isLevelChanging) {
      currentTime = 0f;
      StartCoroutine("levelChange");
    }
    
    if (!isLevelChanging)
      currentTime -= Time.deltaTime;

    timerText.text = currentTime.ToString("0.00");
  }
  IEnumerator levelChange()
  {
    isLevelChanging = true;
    player.freezePlayer();
    anim.SetBool("isPortal", true);
    yield return new WaitForSeconds(levelLoadTime - levelLoadTime/2f);
    float x = 0;
    while(x >= 25)
      x += Time.deltaTime;
      mat.SetFloat("_Float", x);
    yield return new WaitForSeconds(levelLoadTime - levelLoadTime/2f);
    levelChanger.teleportPlayerToArea();
    anim.SetBool("isPortal", false);
    while(x <= 0)
      x -= Time.deltaTime;
      mat.SetFloat("_Float", x);
    yield return new WaitForSeconds(levelLoadTime - levelLoadTime/2f);
    anim.SetBool("isPortal2", true);
    resetTimer();
    yield return new WaitForSeconds(levelLoadTime - levelLoadTime/2f);
    anim.SetBool("isPortal2", false);
    player.releasePlayer();
    isLevelChanging = false;
    yield break;
  }

  void resetTimer() => currentTime = startTime;
}

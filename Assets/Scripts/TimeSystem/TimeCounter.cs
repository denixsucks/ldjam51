using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeCounter : MonoBehaviour
{
  public static TimeCounter Instance { get; private set; }
  const float startTime = 10f;
  const float levelLoadTime = 4f;
  private float currentTime;
  private bool isLevelChanging = false;
  public PlayerMovement player;
  public Levels levelChanger;
  public Text timerText;
  public Animator anim;
  public Animator animShader;
  public GameObject stick;
    public AudioSource portal;
    public AudioSource portal2;
  void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(this);
    }
    else
    {
      Instance = this;
      resetTimer();
    }
  }
  void Update()
  {
    if (currentTime <= 0 && !isLevelChanging)
    {
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
    stick.SetActive(false);
    anim.SetBool("isPortal", true);
    animShader.SetBool("isLevelLoading", true);
    portal.Play();
    yield return new WaitForSeconds(levelLoadTime - levelLoadTime / 2f);
    //yield return new WaitForSeconds(levelLoadTime - levelLoadTime / 2f);
    levelChanger.teleportPlayerToArea();
    animShader.SetBool("isLevelLoading", false);
    animShader.SetBool("isLevelStarted", true);
    anim.SetBool("isPortal", false);
    yield return new WaitForSeconds(2f);
    animShader.SetBool("isLevelStarted", false);
        portal2.Play();
    anim.SetBool("isPortal2", true);
    resetTimer();
    yield return new WaitForSeconds(1f);
    anim.SetBool("isPortal2", false);
    stick.SetActive(true);
    player.releasePlayer();
    isLevelChanging = false;
    yield break;
  }

  void resetTimer() => currentTime = startTime;
}

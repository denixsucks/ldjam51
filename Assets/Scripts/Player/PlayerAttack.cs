using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

  [Header("Attack")]
  public float waitTimeForCombo = 0.5f;
  public float attackDamage = 1f;
  int attackIndex = 0;
  public bool canAttack = true;
  bool timerIsOn = false;
  float timer;

  [Header("Misc")]
  public Animator anim;
  public Camera cam;

  private void Start()
  {
    timer = waitTimeForCombo;
  }
  void Update()
  {
    if (Input.GetMouseButtonDown(0) && canAttack)
    {
      StartCoroutine(Attack());
      timerIsOn = true;
    }

    if (timerIsOn)
    {
      timer -= Time.deltaTime;
    }

    if (timer <= 0f)
    {
      resetTimer();
    }

  }
  IEnumerator Attack()
  {
    if (attackIndex == 0)
    {
      canAttack = false;
      anim.SetInteger("comboCount", 0);
      anim.SetBool("isAttacking", true);
      moveForward();
      giveDamage();
      yield return new WaitForSeconds(0.2f);
      anim.SetBool("isAttacking", false);
      canAttack = true;
      attackIndex = 1;
      yield return null;
    }

    else if (attackIndex == 1)
    {
      canAttack = false;
      anim.SetInteger("comboCount", 1);
      anim.SetBool("isAttacking", true);
      moveForward();
      giveDamage();
      yield return new WaitForSeconds(0.3f);
      anim.SetBool("isAttacking", false);
      canAttack = true;
      attackIndex = 2;
      yield return null;
    }

    else if (attackIndex == 2)
    {
      canAttack = false;
      anim.SetInteger("comboCount", 2);
      anim.SetBool("isAttacking", true);
      moveForward();
      giveDamage();
      yield return new WaitForSeconds(0.5f);
      anim.SetBool("isAttacking", false);
      yield return new WaitForSeconds(1f);
      canAttack = true;
      resetTimer();
      yield return null;
    }
    yield return null;
  }

  void moveForward()
  {
    transform.position = Vector2.MoveTowards(transform.position, cam.ScreenToWorldPoint(Input.mousePosition), 1.5f);
  }
  void resetTimer()
  {
    timerIsOn = false;
    timer = waitTimeForCombo;
    attackIndex = 0;
  }

  void giveDamage()
  {

  }
}
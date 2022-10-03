using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

  [Header("Attack")]
  public float waitTimeForCombo = 0.5f;
  public float attackDamage = 1f;
  public int attackIndex = 0;
  public bool canAttack = true;
  bool timerIsOn = false;
  float timer;

  [Header("Misc")]
  public PlayerMovement move;
  public PlayerThrow playerThrow;
  public GameObject stick;
  public Animator anim;
  public Camera cam;

  private void Start()
  {
    timer = waitTimeForCombo;
  }
  void Update()
  {
       /* 
    if (!canAttack && attackIndex != 2)
    {
      anim.SetBool("isWalking", false);
      move.activeMoveSpeed = 10f;
    }
       */
        if (Input.GetMouseButtonDown(0) && canAttack && !move.frozen)
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
  void hideStickForAttack()
  {
    if(!playerThrow.canCallBack)
    {
      stick.SetActive(false);
    }
  }
  IEnumerator Attack()
  {
    if (attackIndex == 0)
    {
      hideStickForAttack();
      canAttack = false;
      anim.SetInteger("comboCount", 0);
      anim.SetBool("isAttacking", true);
      moveForward(0.4f);
      move.canFlip = false;
      giveDamage();
      yield return new WaitForSeconds(0.2f);
      anim.SetBool("isAttacking", false);
      canAttack = true;
      attackIndex = 1;
      move.canFlip = true;
      move.activeMoveSpeed = move.MovementSpeed;
      stick.SetActive(true);
      yield return null;
    }

    else if (attackIndex == 1)
    {
      hideStickForAttack();
      canAttack = false;
      anim.SetInteger("comboCount", 1);
      anim.SetBool("isAttacking", true);
      moveForward(0.4f);
      move.canFlip = false;
      giveDamage();
      yield return new WaitForSeconds(0.3f);
      anim.SetBool("isAttacking", false);
      canAttack = true;
      attackIndex = 2;
      move.canFlip = true;
      move.activeMoveSpeed = move.MovementSpeed;
      stick.SetActive(true);
      yield return null;
    }

    else if (attackIndex == 2)
    {
      canAttack = false;
      hideStickForAttack();
      anim.SetInteger("comboCount", 2);
      anim.SetBool("isAttacking", true);
      move.activeMoveSpeed = 2f;
      moveForward(0.4f);
      giveDamage();
      move.canFlip = false;
      yield return new WaitForSeconds(0.4f);
      canAttack = true;
      anim.SetBool("isAttacking", false);
      move.activeMoveSpeed = move.MovementSpeed;
      move.canFlip = true;
      stick.SetActive(true);
      yield return new WaitForSeconds(1f);
      resetTimer();
      yield return null;
    }
    yield return null;
  }

  void moveForward(float dist)
  {
    transform.position = Vector2.MoveTowards(transform.position, cam.ScreenToWorldPoint(Input.mousePosition), dist);
    flipFromMouse();
  }
  void flipFromMouse()
  {
    if (cam.ScreenToWorldPoint(Input.mousePosition).x < 0f && move.isFlipped == false)
       move.flipSprite();
    else if (cam.ScreenToWorldPoint(Input.mousePosition).x > 0f && move.isFlipped == true)
      move.flipSprite();
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
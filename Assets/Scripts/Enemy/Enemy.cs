using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
  public EnemyData enemyData;
  protected int maxHealth => enemyData.health;
  protected float speed => enemyData.speed;
  protected int damageValue => enemyData.damageValue;
  public int health {get; set;}
  const float attackDistance = 1.5f;
  const float seeDistance = 10.5f;
  private bool isDead {get; set;}
  private bool isFighting;
  private bool canMove;
  private Transform playerTransform;
  private bool canBeAttacked;
  private bool canAttack;
  private protected virtual void init()
  {
    health = maxHealth;
    canBeAttacked = true;
    canAttack = true;
    isDead = false;
  }
  
  void Start()
  {
    init(); 
  }

  void Update()
  {
    float distanceFromPlayer = Vector2.Distance(transform.position, playerTransform.position);
    if (distanceFromPlayer <= attackDistance) {
      if (isFighting)
        attack();
    }
    else if (attackDistance > distanceFromPlayer && distanceFromPlayer <= seeDistance && canMove){
      isFighting = false;
      moveToPlayer();
    }
    else {
      isFighting = false;
      idle();
    }
  }
  
  void attack()
  {
    if(!canAttack) return;
    if(!isFighting) return;
    isFighting = true;
    StartCoroutine(combat());
  }
  void idle()
  {
    // TODO : IDLE
  }
  
  void moveToPlayer()
  {
    transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, (speed * Time.deltaTime));
  }

  public void damage(int damageValue)
  {
    if (!canBeAttacked)
      return;

    health -= damageValue;

    if (health == 0) {
      die();
    }
  }
 
  void die()
  {
    if (isDead)
      return;

    isDead = true;
    canBeAttacked = false;
    canAttack = false;
  }

  IEnumerator combat()
  {
    // TODO : charge and then dash release attack
    while (health > 0) {
      if (!isFighting) {
        yield break;
      }
      // anim.SetTrigger(AttackTrigger);
      yield return new WaitForSeconds(0.25f);
      // anim.ResetTrigger(AttackTrigger); 
      yield return new WaitForSeconds(1.75f);
    }
  }
}

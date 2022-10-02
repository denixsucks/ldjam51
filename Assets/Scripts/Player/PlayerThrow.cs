using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
  [Header("Rotation")]
  public float rotateSpeed;
  public bool isRotating;


  [Header("Throw")]
  public float throwSpeed;
  public bool isClicked;
  private Vector3 targetPos;

  [Header("CallBack")]
  public Transform playerPos;
  public Transform weapon;
  public bool canCallBack;
  public bool returnWeapon;
  public float maxThrowing = 5f;
  public Camera gameCam;
  public bool canThrow = true;
  bool holding = true;
  public PlayerMovement move;
  public PlayerAttack attack;
  public bool weaponFlipped = false;
  bool weaponCanFlip = true;
  private void Update()
  {
    SelfRotation();

    if (Input.GetMouseButtonDown(1) && !isClicked && canThrow)
    {
      canThrow = false;
      isClicked = true;
      attack.canAttack = false;      
      targetPos = new Vector3(gameCam.ScreenToWorldPoint(Input.mousePosition).x, gameCam.ScreenToWorldPoint(Input.mousePosition).y, 0);
      targetPos = new Vector3(Mathf.Clamp(targetPos.x, playerPos.position.x - maxThrowing, playerPos.position.x + maxThrowing), Mathf.Clamp(targetPos.y, playerPos.position.y - maxThrowing, playerPos.position.y + maxThrowing), 0);
      holding = false;
    }

    if (Input.GetMouseButtonDown(1) && canCallBack)
    {
      returnWeapon = true;
      holding = false;
    }

    if (returnWeapon)
    {
      CallBack();
      isRotating = false;
    }

    if (isClicked && !move.frozen)
    {
      ThrowWeapon();
    }

    if (Vector2.Distance(weapon.transform.position, targetPos) <= 0.1f)
    {
      canCallBack = true;
      isRotating = false;
      weaponCanFlip = false;
    }

    if (holding && !move.frozen) {
      weapon.transform.position = playerPos.position;
      attack.canAttack = true;
    }
    checkIfPlayerNear(0.01f);
  }
  void checkIfPlayerNear(float dist) 
  {
    if (Vector2.Distance(weapon.transform.position, playerPos.position) < dist)
    {
      isRotating = false;
      canThrow = true;
      canCallBack = false;
      returnWeapon = false;
      isClicked = false;     
      holding = true;
      weaponCanFlip = true;
    }
  }
  private void CallBack()
  {
    var PlayerPos = new Vector2(playerPos.position.x + Mathf.Abs(Mathf.Sin(Time.time) * 3f), playerPos.position.y);
    isRotating = true;
    weapon.position = Vector2.MoveTowards(weapon.position, playerPos.position, throwSpeed * 3 * Time.deltaTime);
    checkIfPlayerNear(0.5f);
    weapon.rotation = new Quaternion(0, 0, 0, 0);
  }

  private void ThrowWeapon()
  {
    isRotating = true;
    weapon.position = Vector2.MoveTowards(weapon.position, targetPos, throwSpeed * Time.deltaTime);
  }

  private void SelfRotation()
  {
    if (isRotating)
    {
      weapon.transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
    else
    {
      
    }
  }
  public void flipWeapon()
  {
    if (weaponCanFlip)
      weapon.transform.Rotate(0, 180, 0);
  }


}
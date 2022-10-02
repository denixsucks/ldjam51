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



  private void Update()
  {
    SelfRotation();

    if (Input.GetMouseButtonDown(1) && isClicked == false)
    {
      isClicked = true;
      targetPos = new Vector3(gameCam.ScreenToWorldPoint(Input.mousePosition).x, gameCam.ScreenToWorldPoint(Input.mousePosition).y, 0);
      targetPos = new Vector3(Mathf.Clamp(targetPos.x, playerPos.position.x - maxThrowing, playerPos.position.x + maxThrowing), Mathf.Clamp(targetPos.y, playerPos.position.y - maxThrowing, playerPos.position.y + maxThrowing), 0);

    }

    if (Input.GetMouseButton(1) && canCallBack)
    {
      returnWeapon = true;
    }

    if (returnWeapon)
    {
      CallBack();
      isRotating = false;
    }

    if (isClicked)
    {
      ThrowWeapon();
    }

    if (Vector2.Distance(transform.position, targetPos) <= 0.01f)
    {
      canCallBack = true;

      isRotating = false;
    }


    //burasi bi tik kotu oldu galiba 

    if (Vector2.Distance(transform.position, playerPos.position) <= 0.1f)
    {
      isRotating = false;
      canCallBack = false;
      returnWeapon = false;
      isClicked = false;

      weapon.rotation = new Quaternion(0, 0, 0, 0);
    }

  }

  private void CallBack()
  {
    var PlayerPos = new Vector2(playerPos.position.x + Mathf.Abs(Mathf.Sin(Time.time) * 3f), playerPos.position.y);
    isRotating = true;
    transform.position = Vector2.MoveTowards(weapon.position, PlayerPos, throwSpeed * 3 * Time.deltaTime);
    Debug.Log("Bizim Pos: " + PlayerPos + "   org pos " + playerPos.position);
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
      transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
    else
    {
      transform.Rotate(0, 0, 0);
    }
  }


}
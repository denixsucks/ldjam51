using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

  [Header("Melee")]
  public float attackDamage;
  bool canAttack;

  [Header("Throwable")]
  public GameObject weapon;
  public float mouseLocationTiling = 4.5f;
  public float throwDistance;
  public float throwSpeed;
  bool holding;
  bool inAir;
  Vector3 targetPosition;


  [Header("Rotation")]
  public float rotateSpeed;
  public bool isRotating;


  [Header("Throw")]
  public bool isClicked;
  private Vector3 targetPos;
  public Camera gameCam;

  [Header("CallBack")]
  public bool canCallBack;
  public bool returnWeapon;

  [Header("Misc")]
  [SerializeField] private Transform playerPos;
  // Update is called once per frame
  void Update()
  {
    SelfRotation();

    if (Input.GetMouseButtonDown(1) && isClicked == false)
    {
      isClicked = true;
      targetPos = new Vector3(gameCam.ScreenToWorldPoint(Input.mousePosition).x, gameCam.ScreenToWorldPoint(Input.mousePosition).y + mouseLocationTiling, 0);
      holding = false;
    }

    if (Input.GetMouseButton(1) && canCallBack)
    {
      holding = false;
      returnWeapon = true;
    }

    if (returnWeapon)
    {
      holding = false;
      CallBack();
      isRotating = false;
    }

    if (isClicked)
    {
      holding = false;
      ThrowWeapon();
    }

    if (checkDestination(weapon.transform.position, new Vector2(0f,0f), true))
    {
      inAir = false;
      canCallBack = true;
      isRotating = false;
      isClicked = false;
    }

    if (holding) 
    {
      weapon.transform.position = playerPos.position;
    }

    //burasi bi tik kotu oldu galiba 

    if (checkDestination(weapon.transform.position, playerPos.position, false) && !inAir)
    {
      isRotating = false;
      holding = true;
      canCallBack = false;
      returnWeapon = false;
      isClicked = false;
      weapon.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

  }
  Vector2 calculateDestination()
  {
    float x,y;
    if (targetPos.x < 0)
      x = -throwDistance;
    else 
      x = throwDistance;

    if (targetPos.y < 0)
      y = -throwDistance;
    else
      y = throwDistance;
    
    return new Vector2(playerPos.position.x + x, playerPos.position.y + y);
  }
  bool checkDestination(Vector2 startPos, Vector2 endPos, bool calculate)
  {
    if (calculate)
      endPos = calculateDestination();
    float dif = Vector2.Distance(startPos, endPos);
    return dif <= 0.1f;
  }
  private void CallBack()
  {
    isRotating = true;
    inAir = true;
    weapon.transform.position = Vector2.MoveTowards(weapon.transform.position, playerPos.position, throwSpeed * 3 * Time.deltaTime);
  }

  private void ThrowWeapon()
  {
    isRotating = true;
    inAir = true;
    weapon.transform.position = Vector2.MoveTowards(weapon.transform.position, calculateDestination(), Time.deltaTime * throwSpeed);  
  }
  private void SelfRotation()
  {
    if (isRotating)
    {
      weapon.transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
    else
    {
      weapon.transform.Rotate(0, 0, 0);
    }
  }

}
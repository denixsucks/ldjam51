using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  private Vector2 Movement;
  public bool isFlipped = false;
  public bool canFlip = true;
  bool canMove = true;
  bool canDash = true;
  public bool frozen = false;
  float currentDashTime;



  [Header("Movement")]
  public float MovementSpeed = 5f;
  public Rigidbody2D rb;
  public float activeMoveSpeed;

  [Header("Dash")]
  [SerializeField] float startDashTime = 0.2f;
  [SerializeField] float dashSpeed = 16f;
  public AnimationCurve curve;

  [Header("Misc")]
  public Animator anim;
  public PlayerAttack playerAttack;
  public PlayerThrow playerThrow;

  void Start()
  {
    activeMoveSpeed = MovementSpeed;
  }

  void Update()
  {
    var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    if(Input.GetKeyDown(KeyCode.LeftShift) && canDash && !frozen) {
      anim.SetBool("isDashing", true);
      StartCoroutine(Dash(direction));
    }

    if (direction.x < 0 && !isFlipped && !frozen && canFlip)
      flipSprite();
    else if (direction.x > 0 && isFlipped && !frozen && canFlip)
      flipSprite();

    direction.Normalize();
    if(canMove && direction != Vector2.zero){
      anim.SetBool("isWalking", true);
    }
    else
      anim.SetBool("isWalking", false);
    
  
    if (canMove)
      rb.velocity = direction * activeMoveSpeed;
  }
  
  public void freezePlayer()
  {
    frozen = true;
    canDash = false;
    playerAttack.canAttack = false;
    canMove = false;
    activeMoveSpeed = 0f;
    rb.velocity = Vector2.zero;
    playerThrow.isClicked = true;
    playerThrow.canCallBack = false;
  }
  public void releasePlayer()
  {
    playerAttack.canAttack = true;
    frozen = false;
    canDash = true; 
    canMove = true;
    activeMoveSpeed = MovementSpeed;  
    playerThrow.isClicked = false;
    playerThrow.canCallBack = true;
  }
  IEnumerator Dash(Vector2 direction)
  {
    float curveValue = 0f;
    canDash = false;
    currentDashTime = startDashTime;
    while (currentDashTime > 0f)
    {
      curveValue += Time.deltaTime;
      var x = curve.Evaluate(curveValue);
      currentDashTime -= Time.deltaTime; 
      rb.velocity = direction * dashSpeed * x;
      yield return null;
    }
    anim.SetBool("isDashing", false);
    rb.velocity = new Vector2(0f, 0f); // Stop dashing.
    canDash = true;
  }
  public void flipSprite()
  {
    isFlipped = !isFlipped;
    transform.Rotate(0, 180, 0);

  }
}


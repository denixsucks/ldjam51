using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [Header("Movement")]
  public float MovementSpeed = 5f;
  public Rigidbody2D rb;
  private Vector2 Movement;
  public float activeMoveSpeed;

  [Header("Dash")]
  [SerializeField] float startDashTime = 0.2f;
  [SerializeField] float dashSpeed = 16f;
  float currentDashTime;
  bool canDash = true;
  bool isFlipped = false;
  bool canMove = true;
  bool frozen = false;
  public AnimationCurve curve;
  public Animator anim;
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

    if (direction.x < 0 && !isFlipped && !frozen)
      flipSprite();
    else if (direction.x > 0 && isFlipped && !frozen)
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
    canMove = false;
    activeMoveSpeed = 0f;
    rb.velocity = Vector2.zero;
  }
  public void releasePlayer()
  {
    frozen = false;
    canDash = true; 
    canMove = true;
    activeMoveSpeed = MovementSpeed;  
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
  void flipSprite()
  {
    isFlipped = !isFlipped;
    transform.Rotate(0, 180, 0);

  }
}


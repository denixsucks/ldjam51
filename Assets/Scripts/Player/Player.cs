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
  public AnimationCurve curve;
 
  void Start()
  {
    activeMoveSpeed = MovementSpeed;
  }

  void Update()
  {
    var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    if(Input.GetKeyDown(KeyCode.LeftShift) && canDash) {
      StartCoroutine(Dash(direction));
    }
    direction.Normalize();
    rb.velocity = direction * activeMoveSpeed;
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
    rb.velocity = new Vector2(0f, 0f); // Stop dashing.
    canDash = true;
  }
}


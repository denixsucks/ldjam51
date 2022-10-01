using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [Header("Movement")]
  public float MovementSpeed = 5f;
  public Rigidbody2D rb;
  private Vector2 Movement;
  private float activeMoveSpeed;

  [Header("Dash")]
  [SerializeField] float startDashTime = 1f;
  [SerializeField] float dashSpeed = 1f;
  float currentDashTime;
  bool canDash = true;
 
  void Start()
  {
    activeMoveSpeed = MovementSpeed;
  }

  void Update()
  {
    var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    if(Input.GetKeyDown(KeyCode.LeftShift)) {
      StartCoroutine(Dash(direction));
    }
    direction.Normalize();
    rb.velocity = direction * activeMoveSpeed;
  }
  
  IEnumerator Dash(Vector2 direction)
  {
    canDash = false;
    currentDashTime = startDashTime;
    while (currentDashTime > 0f)
    {
      currentDashTime -= Time.deltaTime; 
      rb.velocity = direction * dashSpeed;
      yield return null;
    }
    rb.velocity = new Vector2(0f, 0f); // Stop dashing.
    canDash = true;
  }
}


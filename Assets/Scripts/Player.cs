using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float MovementSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 Movement;


    
    [Header("Dash")]
    private float activeMoveSpeed;
    public float dashSpeed;

    public float dashLength = .5f, dashCoolDown = 1f;

    private float dashCounter;
    private float dashCoolCounter;

    


    void Start()
    {
        activeMoveSpeed = MovementSpeed;
    }
    
    void Update()
    {

        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");

        Movement.Normalize();

        
        rb.velocity = Movement * activeMoveSpeed;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = MovementSpeed;
                dashCoolCounter = dashCoolDown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
        
    }



    /*
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Movement.normalized * MovementSpeed * Time.deltaTime);
        //rb.velocity = Movement.normalized * Time.fixedDeltaTime * MovementSpeed * 100;
    }
    */
}

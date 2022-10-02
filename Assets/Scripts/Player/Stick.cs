using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stick : MonoBehaviour
{
    [Header("Rotation")]
    public float rotateSpeed;
    public bool isRotating;


    [Header("Throw")]
    public float throwSpeed;
    public bool isClicked;
    private Vector3 targetPos;
    public Camera gameCam;

    [Header("CallBack")]
    public Transform playerPos;
    public bool canCallBack;
    public bool returnWeapon;


    private void Update()
    {
        
        SelfRotation();

        if (Input.GetMouseButtonDown(0) && isClicked == false)
        {
            isClicked = true;
            targetPos = new Vector3(gameCam.ScreenToWorldPoint(Input.mousePosition).x, gameCam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }

        if (Input.GetMouseButton(0) && canCallBack)
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

            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

    }

    private void CallBack()
    {
        isRotating = true;
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, throwSpeed * 3 * Time.deltaTime);


    }

    private void ThrowWeapon()
    {
        isRotating = true;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, throwSpeed * Time.deltaTime);
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
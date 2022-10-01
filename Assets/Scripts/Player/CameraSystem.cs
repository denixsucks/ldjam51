using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 offset;
    public Transform player;

    private float _camSize;
    private Camera cam;

    public float maxSize;
    public float minSize;

    private void Start()
    {
        cam = GetComponent<Camera>();
        _camSize = cam.orthographicSize;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, moveSpeed * Time.deltaTime);
        Zoom();
    }

    private void Zoom()
    {
        if (Mathf.Abs(cam.orthographicSize - _camSize) > 0.05)
        {
            float change = Mathf.Lerp(cam.orthographicSize, _camSize, Time.deltaTime * 2);
            cam.orthographicSize = change;
        }
        if (Input.mouseScrollDelta.y > 0 && _camSize > minSize)
        {
            _camSize -= 1;
        }
        else if (Input.mouseScrollDelta.y < 0 && _camSize < maxSize)
        {
            _camSize += 1;
        }
    }
}

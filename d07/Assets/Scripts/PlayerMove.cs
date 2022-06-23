using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // [SerializeField]
    // private float _mouseSensitivity = 0.4f;
    [SerializeField]
    private float _moveSpeed = 10f;

    private Vector3 _mousePreveousePos;
    private float _rotationX;
    private float _rotationY;

    void Update()
    {
        if (Input.GetKey("a"))
        {
            transform.Rotate(0, -0.3f, 0,  Space.World);
        }
        if (Input.GetKey("d"))
        {
            transform.Rotate(0, 0.3f, 0,  Space.World);
        }
        Move();
    }

    void Move() 
    {

        float shiftMult = 1f;
        if (Input.GetKey(KeyCode.LeftShift)) {
            shiftMult = 1.2f;
        }

        float right = 0;
        float forward = Input.GetAxis("Vertical");
        float up = 0;
        if (Input.GetKey("8")) {
            forward = 1f;
        } else if (Input.GetKey("2")) {
            forward = -1f;
        }

        Vector3 offset = new Vector3(right, up, forward) * _moveSpeed * shiftMult * Time.unscaledDeltaTime;
        transform.Translate(offset);
    }
}
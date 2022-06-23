using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;

    private Vector3 _mousePreveousePos;
    private float _rotationX;
    private float _rotationY;
    private bool move;
    private GameObject player;
    private Quaternion rotation;

    void Start()
    {
        move = true;
        InvokeRepeating("move_stop", 0.5f,3);
        InvokeRepeating("choose_rotate", 0, 5);
        player = GameObject.Find("player_body");
    }

    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(1);
        }
        Move();
        Rotate();
    }

    void Move() 
    {
        float right = 0;
        float forward = Input.GetAxis("Vertical");
        float up = 0;
        forward = 1f;
        if (move)
        {
            Vector3 offset = new Vector3(right, up, forward) * _moveSpeed * Time.unscaledDeltaTime;
            transform.Translate(offset);
        }
    }

    void Rotate()
    {
        int speed_rotate;

        System.Random rand = new System.Random();
        speed_rotate = rand.Next(0, 10);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed_rotate); 
    }

    void choose_rotate()
    {
        int i;

        System.Random rand = new System.Random();
        i = rand.Next(0, 10);
        if (i <= 2)
        {
            Vector3 lookPos = player.transform.position - transform.position;
            lookPos.y = 0;
            rotation = Quaternion.LookRotation(lookPos);
        }
        else if (i > 7)
        {
            Vector3 lookPos = new Vector3(200f, 260f, 170f) - transform.position;
            lookPos.y = 0;
            rotation = Quaternion.LookRotation(lookPos);
        }
        else if (i > 5)
        {
            Vector3 lookPos = new Vector3(205f, 250f, 105f) - transform.position;
            lookPos.y = 0;
            rotation = Quaternion.LookRotation(lookPos);
        }
        else
        {
            Vector3 lookPos = new Vector3(135f, 250f, 130f) - transform.position;
            lookPos.y = 0;
            rotation = Quaternion.LookRotation(lookPos);
        }
    }

    void move_stop()
    {
        int i;

        System.Random rand = new System.Random();
        i = rand.Next(0, 10);
        if (i < 3)
        {
            move = false;
            Debug.Log(transform.name + " stop");
        }
        if (i >= 3)
        {
            move = true;
            Debug.Log(transform.name + " move");
        }
    }
}
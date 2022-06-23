using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tank_life : MonoBehaviour
{
    public int health;
    public GameObject tank;
    
    void Start()
    {
        health = 100;
        Debug.Log(health);
    }

    public void take_dmg(int damage)
    {
        health -= damage;
    }

    void Update()
    {
        if (health <= 0)
        {
            
            Destroy(tank);
        }
    }

    public string __str__()
    {
        return Convert.ToString(health);
    }
}

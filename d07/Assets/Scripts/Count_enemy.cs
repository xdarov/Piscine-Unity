using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Count_enemy : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("l")) // Delete <--
        {
            // Debug.Log(transform.GetComponents(typeof(Component))[2]);
            Debug.Log(transform.GetComponent<UnityEngine.UI.Text>().text);
        }
        transform.GetComponent<UnityEngine.UI.Text>().text = "Enemis: " + Convert.ToString(GameObject.FindGameObjectsWithTag("Enemy").Length / 3);
    }
}

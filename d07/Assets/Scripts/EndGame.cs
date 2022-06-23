using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            SceneManager.LoadScene(2);
        }
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {
            SceneManager.LoadScene(3);
        }
    }
}

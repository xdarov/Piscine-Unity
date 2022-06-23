using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelthBar : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        transform.GetComponent<UnityEngine.UI.Text>().text = "Health: " + player.transform.GetComponent<Tank_life>().__str__();
    }
}
// Health (UnityEngine.UI.Text)



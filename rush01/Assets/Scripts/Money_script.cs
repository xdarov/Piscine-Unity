using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money_script : MonoBehaviour
{
    public Move_maya player;
    public Text txt;

    // Update is called once per frame
    void Update()
    {
        txt.text = "MONEY " + player.money;
    }
}

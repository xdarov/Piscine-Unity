using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class xp_script : MonoBehaviour
{
    public Slider slider;
    public Move_maya player;
    public Text txt;

    // Use this for initialization
    void Start()
    {
        slider.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        slider.maxValue = player.xpForNext;
        slider.value = player.xp;
        txt.text = Mathf.Clamp(player.xp, 0, player.xpForNext) + "/" + player.xpForNext;
    }
}

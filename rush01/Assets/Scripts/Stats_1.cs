using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats_1 : MonoBehaviour
{

    public Move_maya player_stats;
    public Text txt;

    // Update is called once per frame
    void Update()
    {
        txt.text = "FOR = " + player_stats.STR + "\n\nAGI = " + player_stats.AGI + "\n\nCON = " + player_stats.CON + "\n\ndmgMin = " + player_stats.minDMG + "\n\ndmgMax = " + player_stats.maxDMG;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ennemy_script : MonoBehaviour
{

    public Move_maya player;
    public Slider slider;
    public Text txt;
    public GameObject slider_go;
    // Use this for initialization
    void Start()
    {
        slider_go.SetActive(false);
        txt.enabled = false;
        slider.minValue = 0;
        txt.text = "Zombie";
    }

    // Update is called once per frame
    void Update()
    {
        if (player.hit.collider != null)
        {
            if ((player.hit.collider.tag == "zombie" || player.animator.GetInteger("State") == 2) && player.zombie_stats.hp > 0)
            {
                Debug.Log("Im in babe");
                slider_go.SetActive(true);
                txt.enabled = true;
                slider.maxValue = player.zombie_stats.CON * 5;
                slider.value = player.zombie_stats.hp;
                txt.text = "Zombie lvl. " + player.zombie_stats.level;
            }
            else
            {
                txt.enabled = false;
                slider_go.SetActive(false);
            }
        }
    }
}

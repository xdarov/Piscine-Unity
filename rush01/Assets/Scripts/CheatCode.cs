using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCode : MonoBehaviour
{
    public GameObject Weapon;
    public GameObject Player;

    void Start()
    {
        Player = GameObject.Find("Maya");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager.gm.Player.LevelUp();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject tmp = Instantiate(Weapon, Player.gameObject.transform.position, Weapon.transform.rotation);
            tmp.transform.position = Player.gameObject.transform.position;
        }
    }
}

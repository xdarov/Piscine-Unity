using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject Weapon;
    public GameObject Player;
    public AudioSource bay;
    public AudioSource nomoney;


    void Start()
    {
        Player = GameObject.Find("Maya");
        // Weapon = GameObject.Find("Weapons");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            int money = Player.GetComponent<Move_maya>().money;
            if (money >= 10)
            {
                Player.GetComponent<Move_maya>().money -= 10;
                bay.Play();
                GameObject tmp = Instantiate(Weapon, Player.gameObject.transform.position, Weapon.transform.rotation);
                tmp.transform.position = Player.gameObject.transform.position;
            }
            else
                nomoney.Play();
        }
    }
}

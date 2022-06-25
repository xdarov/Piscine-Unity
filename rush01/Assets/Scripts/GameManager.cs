using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public Transform player;
    public float y;
    public bool dropping = false;
    public Move_maya Player;
    public Inventory Inventory;
    public List<GameObject> WeaponSkin;
    public List<SpriteRenderer> WeaponIcon;
    public bool Dropping;

    // Use this for initialization
    void Awake()
    {
        y = 9.5f;
        if (gm != null && gm != this)
            Destroy(gameObject);    // Suppression d'une instance précédente (sécurité...sécurité...)

        gm = this;
        if (!Player)
            Player = GameObject.Find("Maya").GetComponent<Move_maya>();
        if (!Inventory)
            Inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position + new Vector3(0, y, 10);
    }
}
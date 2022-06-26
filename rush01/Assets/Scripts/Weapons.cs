using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
    public float AttackMin;
    public float AttackMax;

    public float Damage;
    public float AttackSpeed;
    public float AttackSpeedMin;
    public float AttackSpeedMax;
    public RarityLevel Rarity;

    public ToolTip ToolTip;
    private GameObject obj_halo;

    public enum RarityLevel
    {
        Common,
        NonCommon,
        Magic,
        Legendaire,
        Length
    }

    // Use this for initialization
    void Start()
    {
        SetRandomSkins();
        SetRandomStats();
    }

    // Update is called once per frame
    void Update()
    {
        ToolTip.toolTip = string.Format("Damage: " + Damage.ToString() + System.Environment.NewLine + "Attack speed: " + AttackSpeed.ToString() + System.Environment.NewLine + "Rarity" + Rarity.ToString());
    }

    private void OnMouseOver()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            UiManager.ToolTip = "";
            GameManager.gm.Inventory.AddItem(this.gameObject);
        }
    }

    public void SetRandomSkins()
    {
        int index = Random.Range(0, GameManager.gm.WeaponSkin.Count);
        GameObject clone = Instantiate(GameManager.gm.WeaponSkin[index], transform.position, Quaternion.identity);
        gameObject.GetComponent<Image>().sprite = GameManager.gm.WeaponIcon[index].sprite;
        clone.gameObject.transform.position = this.gameObject.transform.position;
        clone.gameObject.transform.SetParent(this.transform);
    }

    public void SetRandomStats()
    {
        int i;
        var halo_list = new List<string>() { "Halo_common", "Halo_noncommon", "Halo_magic", "Halo_legendary" };

        ToolTip = gameObject.GetComponent<ToolTip>();
        Damage = Random.Range(AttackMin, AttackMax);
        AttackSpeed = Random.Range(AttackSpeedMin, AttackSpeedMax);
        AttackSpeed *= GameManager.gm.Player.level * 1.5f;
        i = Random.Range(0, 4);
        Rarity = (RarityLevel)i;
        obj_halo = Instantiate(GameObject.Find(halo_list[i]));
        obj_halo.transform.position = transform.position;
        obj_halo.gameObject.transform.SetParent(this.transform);
        Damage *= GameManager.gm.Player.level * 1.5f * (1f + ((float)i / 10f));
    }
}
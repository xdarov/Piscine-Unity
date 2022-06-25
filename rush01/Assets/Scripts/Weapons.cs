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

    public enum RarityLevel
    {
        Legendaire,
        Magique,
        NonCommun,
        Commun,
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
        ToolTip = gameObject.GetComponent<ToolTip>();
        Damage = Random.Range(AttackMin, AttackMax);
        AttackSpeed = Random.Range(AttackSpeedMin, AttackSpeedMax);
        AttackSpeed *= GameManager.gm.Player.level * 1.5f;
        Damage *= GameManager.gm.Player.level * 1.5f;
        for (int i = 1; i < (int)RarityLevel.Length; i++)
        {
            float random = Random.Range(0f, 1f);
            if (random <= (float)i / (float)RarityLevel.Length)
            {
                Rarity = (RarityLevel)i;
                return;
            }
        }
    }
}

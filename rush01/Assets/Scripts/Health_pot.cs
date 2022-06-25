using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_pot : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("TRIGGERED");
        Move_maya maya = other.GetComponent<Move_maya>();
        if (maya)
        {
            maya.hp = (int)Mathf.Clamp(maya.hp + (int)(maya.CON * 5 * 0.3f), 0, (maya.CON * 5));
        }
        Destroy(this.gameObject);
    }
}

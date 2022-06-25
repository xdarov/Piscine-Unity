using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public void AddItem(GameObject newItem)
    {
        foreach (Transform cell in transform)
        {
            if (cell.childCount > 0)
                continue;
            newItem.transform.SetParent(cell);
            newItem.transform.position = cell.position;
            return;
        }
    }
}

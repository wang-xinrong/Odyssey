using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData.asset", menuName = "Data/InventoryData")]
public class InventoryData : ScriptableObject
{
    public Item[] Items;
    public int[] Quantities;
}

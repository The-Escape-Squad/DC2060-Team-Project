using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Basic")]
public class ItemSO : ScriptableObject
{

    public string itemName;
    public string itemDescription;
    public Sprite itemGraphic;

}

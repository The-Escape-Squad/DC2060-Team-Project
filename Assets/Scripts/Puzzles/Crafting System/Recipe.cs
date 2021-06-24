using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Item/Recipe")]
public class Recipe : ScriptableObject
{

    public List<ItemSO> ingredients = new List<ItemSO>();
    public ItemSO result;

}

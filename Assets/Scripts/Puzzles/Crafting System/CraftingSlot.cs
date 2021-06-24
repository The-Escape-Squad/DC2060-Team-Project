using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSlot : MonoBehaviour, IDragInteractable
{

    [Header("References")]
    public ItemSO item;
    public SpriteRenderer imageGraphic;

    public void UseItem(ItemSO item)
    {
        UpdateItem(item);
    }

    public void UpdateItem(ItemSO item)
    {
        // Update the item in the slot
        this.item = item;
        if (item != null)
        {
            imageGraphic.sprite = item.itemGraphic;
            imageGraphic.enabled = true;
        }
        else
        {
            imageGraphic.enabled = false;
        }
    }

}

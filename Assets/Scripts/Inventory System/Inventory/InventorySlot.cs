using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerClickHandler
{

    [Header("References")]
    private Inventory inventory;
    public Image itemSlot;
    private ItemSO myItem;

    public void Init(Inventory inventory)
    {
        this.inventory = inventory;
        itemSlot.sprite = null;
        myItem = null;
    }

    public void UpdateItem(ItemSO newItem)
    {
        myItem = newItem;
        itemSlot.sprite = (myItem == null) ? null : myItem.itemGraphic;
    }

    public bool IsEmpty()
    {
        return myItem == null;
    }

    // UI Interaction Methods
    public void OnPointerClick(PointerEventData eventData)
    {
        // Temporarily, when clicked we will make it so that it uses the item
        // i.e. removes it from the inventory

        if(myItem != null)
        {
            Debug.Log("Used Item: " + myItem.itemName);
            UpdateItem(null);
            inventory.UpdateTooltip("No Item");
        } else
        {
            Debug.Log("There's no item to use here...");
        }

        // TODO - Create a system for the Player by which it can be assigned a
        // current item to use
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(myItem)
        {
            inventory.UpdateTooltip(myItem.itemName, myItem.itemDescription);
        } else
        {
            inventory.UpdateTooltip("No Item");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventory.UpdateTooltip("Tooltip");
    }

}

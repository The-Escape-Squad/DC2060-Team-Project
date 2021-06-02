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
    public Image selectedIndicator;
    private ItemSO myItem;
    public AudioClip hoverOverClip;

    public void Init(Inventory inventory)
    {
        this.inventory = inventory;
        itemSlot.sprite = null;
        myItem = null;
        selectedIndicator.enabled = false;
    }

    public void UpdateItem(ItemSO newItem)
    {
        myItem = newItem;
        if(myItem)
        {
            itemSlot.sprite = myItem.itemGraphic;
            itemSlot.enabled = true;
        } else
        {
            itemSlot.sprite = null;
            itemSlot.enabled = false;
        }   
    }

    public ItemSO GetItem()
    {
        return myItem;
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
            if(myItem is ViewableItemSO viewItem) {
                GameObject.FindGameObjectWithTag(viewItem.viewScreenTag).GetComponent<Canvas>().enabled = true;
                inventory.ToggleState();
            }
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
        GameManager.Instance.audioManager.PlaySoundOneShot(hoverOverClip);
        selectedIndicator.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventory.UpdateTooltip("Tooltip");
        selectedIndicator.enabled = false;
    }

}

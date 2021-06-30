using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [Header("References")]
    private Inventory inventory;
    public Image itemSlot;
    public Image selectedIndicator;
    [SerializeField] private ItemSO myItem;
    public AudioClip hoverOverClip;

    [Header("Item Usage")]
    private bool holdingItem;

    public class ItemUsageData
    {
        public Vector2 mousePosition;
        public ItemSO usedItem;

        public ItemUsageData(Vector2 mousePosition, ItemSO usedItem)
        {
            this.mousePosition = mousePosition;
            this.usedItem = usedItem;
        }
    }

    public void Init(Inventory inventory)
    {
        this.inventory = inventory;
        itemSlot.sprite = null;
        selectedIndicator.enabled = false;
        // Updates visuals with item on Initialisation
        UpdateItem(myItem);
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

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Ending Drag");
        if (holdingItem)
        {
            // Perform a raycast to see if we hit an interactable that takes items
            RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (hit)
            {
                Debug.Log("Hit Something");
                IDragInteractable interactable;
                if((interactable = hit.transform.GetComponent<IDragInteractable>()) != null)
                {
                    Debug.Log("Interacting");
                    interactable.UseItem(myItem);
                }
            }
            ActiveItemTracker.Instance.UpdateSelectedItem(null);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Trying Drag");
        if(myItem != null)
        {
            Debug.Log("Has Item");
            // If there is an item in this slot and the item is usable, we want
            // to assign it to the ActiveItemTracker and set the icon to follow
            if(myItem is UsableItemSO usableItem)
            {
                Debug.Log("Item is Draggable, performing drag");
                ActiveItemTracker.Instance.UpdateSelectedItem(usableItem);
                holdingItem = true;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Do nothing, required for drag support but nothing
        // needs doing here
    }
}

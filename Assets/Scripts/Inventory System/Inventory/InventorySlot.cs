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

    public void Init(Inventory inventory)
    {
        this.inventory = inventory;
        itemSlot.sprite = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inventory.UpdateTooltip("No Item");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventory.UpdateTooltip("Tooltip");
    }

}

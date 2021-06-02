using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{

    [Header("References")]
    public InventorySlot[] slots;
    public Animator uiAnimator;

    public TextMeshProUGUI tooltipTitle;
    public TextMeshProUGUI tooltipDescription;

    // This is a temporary item for the purposes of testing item giving + usage
    public ItemSO testItem;

    private bool state = false;

    [Header("Effects")]
    public AudioClip inventoryOpenSound;
    public AudioClip inventoryCloseSound;

    public void Start()
    {
        foreach(InventorySlot slot in slots)
        {
            slot.Init(this);
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            AddItem(testItem);
        }
    }

    public void ToggleState()
    {
        state = !state;
        uiAnimator.SetBool("Open", state);
        GameManager.Instance.audioManager.PlaySoundOneShot(state ? inventoryOpenSound : inventoryCloseSound);
    }

    public void UpdateTooltip(string itemName, string itemDescription = "")
    {
        tooltipTitle.text = itemName;
        tooltipDescription.text = itemDescription;
    }

    /// <summary>
    /// A method to add an item to the inventory and returning the index
    /// at which said item has been added
    ///
    /// If there is no space available, this method returns -1
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public void AddItem(ItemSO item)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i].IsEmpty())
            {
                slots[i].UpdateItem(item);
                break;
            }
        }
    }

    public int HasItem(ItemSO item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetItem() != null && slots[i].GetItem() == item)
            {
                return i;
            }
        }

        return -1;
    }

}

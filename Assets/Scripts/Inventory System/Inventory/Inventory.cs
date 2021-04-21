using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{

    [Header("References")]
    public InventorySlot[] slots;
    public Animator uiAnimator;
    public AudioSource inventoryAudioSource;

    public TextMeshProUGUI tooltipTitle;
    public TextMeshProUGUI tooltipDescription;

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

    public void ToggleState()
    {
        state = !state;
        uiAnimator.SetBool("Open", state);
        inventoryAudioSource.PlayOneShot(state ? inventoryOpenSound : inventoryCloseSound);
    }

    public void UpdateTooltip(string itemName, string itemDescription = "")
    {
        tooltipTitle.text = itemName;
        tooltipDescription.text = itemDescription;
    }

}

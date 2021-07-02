using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListInteractable : Interactable
{
    public ItemSO[] requiredItems;
    public string noItemMessage;

    protected override void OnInteract()
    {
        foreach(ItemSO item in requiredItems)
        {
            if (Player.Instance.playerInventory.HasItem(item) == -1)
            {
                Player.Instance.notificationWindow.DisplayMessage(noItemMessage);
                return;
            }
        }
        base.OnInteract();
    }
}

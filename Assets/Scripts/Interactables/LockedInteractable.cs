using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedInteractable : Interactable
{

    public ItemSO requiredItem;
    public string noItemMessage;

    protected override void OnInteract()
    {
        if(Player.Instance.playerInventory.HasItem(requiredItem) != -1)
        {
            base.OnInteract();
        } else
        {
            Player.Instance.notificationWindow.DisplayMessage(noItemMessage);
        }
    }
}

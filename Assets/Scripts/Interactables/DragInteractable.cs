using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragInteractable : Interactable, IDragInteractable
{
    public ItemSO requiredItem;
    public string wrongItemMessage = "I can't use this here...";
    public string noItemMessage = "I need an item to interact with this";

    protected override void OnInteract()
    {
        Player.Instance.notificationWindow.DisplayMessage(noItemMessage);
    }

    public void UseItem(ItemSO item)
    {
        if(item == requiredItem)
        {
            base.OnInteract();
        } else
        {
            Player.Instance.notificationWindow.DisplayMessage(wrongItemMessage);
        }
    }
}

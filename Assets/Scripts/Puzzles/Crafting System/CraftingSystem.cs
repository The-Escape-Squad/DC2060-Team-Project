using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{

    [Header("References")]
    public Recipe recipe;
    public CraftingSlot[] slots;

    public string successMessage;

    public void TryCraft()
    {
        // Fill list with items for crafting
        List<ItemSO> itemsInSlots = new List<ItemSO>();
        foreach(CraftingSlot slot in slots)
        {
            if(slot.item == null)
            {
                // Display a message saying we need more stuff
                Player.Instance.notificationWindow.DisplayMessage("I need more things to craft something...");
                return;
            }
            itemsInSlots.Add(slot.item);
        }

        // Check if items line up with recipe
        List<ItemSO> recipeItems = recipe.ingredients;
        foreach (ItemSO item in itemsInSlots)
        {
            if(recipeItems.Contains(item))
            {
                recipeItems.Remove(item);
            }
        }

        if(recipeItems.Count == 0)
        {
            // Craft item
            foreach(CraftingSlot slot in slots)
            {
                Player.Instance.playerInventory.RemoveItem(slot.item);
                slot.UpdateItem(null);
            }
            Player.Instance.playerInventory.AddItem(recipe.result);
            Player.Instance.notificationWindow.DisplayMessage(successMessage);
        } else
        {
            // Message for failure
            Player.Instance.notificationWindow.DisplayMessage("These items don't make anything...");
        }
    }

}

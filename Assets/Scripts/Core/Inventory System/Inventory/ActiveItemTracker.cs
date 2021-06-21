using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveItemTracker : MonoBehaviour
{

    public static ActiveItemTracker Instance { get; private set; }
    public ItemSO selectedItem = null;
    public Image selectedItemImage;

    public void Awake()
    {
        // Set Up Game Manager Singleton
        DontDestroyOnLoad(this);
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public void UpdateSelectedItem(ItemSO selectedItem)
    {
        this.selectedItem = selectedItem;
        if(selectedItem != null)
        {
            selectedItemImage.sprite = selectedItem.itemGraphic;
            selectedItemImage.enabled = true;
        } else
        {
            selectedItemImage.enabled = false;
        }
    }

    public void Update()
    {
        selectedItemImage.transform.position = Input.mousePosition;
    }

}

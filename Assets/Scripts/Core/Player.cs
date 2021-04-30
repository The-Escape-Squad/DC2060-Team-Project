using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Singleton Fields
    private static Player instance;
    public static Player Instance
    {
        get
        {
            return instance;
        }
    }

    [Header("References")]
    public Inventory playerInventory;
    public NotificationWindow notificationWindow;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }

}

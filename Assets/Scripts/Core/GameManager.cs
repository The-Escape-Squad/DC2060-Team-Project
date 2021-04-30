using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public void Awake()
    {
        // Set Up Game Manager Singleton
        DontDestroyOnLoad(this);
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        if (Application.isEditor)
        {
            Debug.Log("In Editor - Won't Close");
        } else
        {
            Application.Quit();
        }
    }
 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject[] cameras;
    private int index;

    public void TurnRight()
    {
        if (index >= cameras.Length-1)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        UpdateCameras();
    }

    public void TurnLeft()
    {
        if (index == 0)
        {
            index = cameras.Length-1;
        }
        else
        {
            index--;
        }
        UpdateCameras();
    }

    public void UpdateCameras()
    { 
        for(int i = 0; i<cameras.Length; i++)
        {
            if(i == index)
            {
                cameras[i].SetActive(true);
            }
            else
            {
                cameras[i].SetActive(false);
            }
        }
    }
}

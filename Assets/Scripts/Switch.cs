using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool isOn;

    private void onMouseUp()
    {
        // if statement
        Main.Instance.SwitchChange(isOn ? 1 : -1);
    }
}

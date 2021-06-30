using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceInteractable : Interactable
{
    // array of item needs to have, increase index once have the wire 
    private int indx = 0;
    private string[] hints = new string[] {"Walls were made to be broken", "Thaw the freeze with a high degrees", "Inside the machine you will find the green"};

    protected override void OnInteract()
    {
            Player.Instance.notificationWindow.DisplayMessage(hints[indx]);

        indx = (indx >= 2) ? 0 : indx + 1;
     
    }
}

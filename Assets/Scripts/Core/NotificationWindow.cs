using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationWindow : MonoBehaviour
{

    [Header("References")]
    public Animator animator;
    public TextMeshProUGUI displayText;

    public void DisplayMessage(string message)
    {
        displayText.text = message;
        animator.SetTrigger("Display");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    public UnityEvent onInteractEvent = new UnityEvent();
    public bool isInteractable = true;

    public string interactionMessage;

    public AudioClip interactionClip;

    public void OnMouseDown()
    {
        if(isInteractable)
        {
            OnInteract();
        }
    }

    protected virtual void OnInteract()
    {
        if (interactionClip) GameManager.Instance.audioManager.PlaySoundOneShot(interactionClip);
        Player.Instance.notificationWindow.DisplayMessage(interactionMessage);
        onInteractEvent?.Invoke();
    }

    public void SetInteractable(bool interactable)
    {
        isInteractable = interactable;
    }
}

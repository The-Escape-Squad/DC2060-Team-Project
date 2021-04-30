using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    public UnityEvent onInteractEvent = new UnityEvent();
    public bool isInteractable = true;

    public virtual void OnMouseDown()
    {
        if(isInteractable)
        {
            Debug.Log("You clicked on: " + transform.name);
            onInteractEvent?.Invoke();
        }
    }

    public void SetInteractable(bool interactable)
    {
        isInteractable = interactable;
    }
}

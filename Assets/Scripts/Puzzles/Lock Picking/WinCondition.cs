using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinCondition : MonoBehaviour
{
    public UnityEvent winEvent = new UnityEvent();

    void OnTriggerEnter2D(Collider2D collider)
    {
        winEvent.Invoke();
    }
}

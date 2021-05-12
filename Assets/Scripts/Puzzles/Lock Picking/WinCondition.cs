using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinCondition : MonoBehaviour
{
    public UnityEvent winEvent = new UnityEvent();
    public AudioClip unlockClip;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Ball"))
        {
            Debug.Log("Lock Has Been Picked");
            GameManager.Instance.audioManager.PlaySoundOneShot(unlockClip);
            winEvent.Invoke();
        }
    }
}

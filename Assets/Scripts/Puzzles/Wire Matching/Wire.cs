using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{

    [Header("Wire Data")]
    public Color wireColor;
    [Range(0, 2)]
    public int wireMatchIndex;
    public bool canInteract = true;

    [Header("References")]
    public LineRenderer wire;
    public SpriteRenderer wireSocket;
    public GameObject lightOn;
    public GameObject wireTip;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        wire.SetPosition(0, transform.position);
        wire.SetPosition(1, transform.position);
        wire.startColor = wireColor;
        wire.endColor = wireColor;
        wireSocket.color = wireColor;
    }

    private void OnMouseDrag()
    {
        if(canInteract)
        {
            // Get mouse position
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = mousePosition;
            newPosition.z = 0;

            // Check for connection points
            Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePosition, .2f);
            foreach (Collider2D collider in colliders)
            {
                Wire newWire;
                if ((newWire = collider.GetComponent<Wire>()) != null)
                {
                    if (newWire != this && newWire.wireMatchIndex == this.wireMatchIndex)
                    {
                        // count connection
                        WireManager.Instance.SwitchChange(1);

                        newWire.Done();
                        Done();
                        newPosition = newWire.transform.position;
                    }
                }
            }

            UpdateWire(newPosition);
        }
    }

    void Done()
    {
        // turn on light
        lightOn.SetActive(true);
        canInteract = false;
    }

    private void OnMouseUp()
    {
        if(canInteract)
        {
            UpdateWire(startPosition);
        }
    }

    void UpdateWire(Vector3 newPosition)
    {
        // Update the wire size
        wire.SetPosition(1, newPosition);
        wire.SetPosition(0, transform.position);

        // Update position and rotation of the tip
        wireTip.transform.position = newPosition;
        wireTip.transform.right = newPosition - startPosition;
    }
}

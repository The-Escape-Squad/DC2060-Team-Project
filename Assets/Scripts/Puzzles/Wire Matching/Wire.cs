using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public SpriteRenderer wireEnd;
    public GameObject lightOn;
    Vector3 startPoint;
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        startPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        // mouse position
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        // check connection points
        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, .2f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                UpdateWire(collider.transform.position);

                // check wires match
                if(transform.parent.name.Equals(collider.transform.parent.name))
                {
                    // count connection
                    Main.Instance.SwitchChange(1);

                    collider.GetComponent<Wire>()?.Done();
                    Done();
                } 

                return;
            }
        }

        UpdateWire(newPosition);
    }

    void Done()
    {
        // turn on light
        lightOn.SetActive(true);

        // disable
        Destroy(this);
    }

    private void OnMouseUp()
    {
        UpdateWire(startPosition);

    }
    void UpdateWire(Vector3 newPosition)
    {

        // update wire and update position 
        transform.position = newPosition;

        // update position 
        Vector3 direction = newPosition - startPoint;
        // update position on right side 
        transform.right = direction * transform.lossyScale.x;

        // update scale
        float distance = Vector2.Distance(startPoint, newPosition);
        wireEnd.size = new Vector2(distance, wireEnd.size.y);
    }
}

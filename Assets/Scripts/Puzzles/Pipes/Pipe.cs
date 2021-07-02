using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{

    [Header("References")]
    public bool isConnected;
    public SpriteRenderer pipeRenderer;
    public Transform[] connectionPoints;
    public LayerMask connectionsLayer;

    [Header("Pipe Data")]
    public Color connectedColour;
    public Color disconnectedColour;
    public List<Pipe> connections = new List<Pipe>();

    public void UpdateConnections()
    {
        connections.Clear();
        foreach(Transform connectionPoint in connectionPoints)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(connectionPoint.transform.position, connectionPoint.transform.up, 0.1f, connectionsLayer);
            foreach(RaycastHit2D hit in hits)
            {
                if (hit && hit.transform != this.transform)
                {
                    Debug.Log("Hit Adjacent Pipe");
                    connections.Add(hit.transform.GetComponent<Pipe>());
                }
            }
        }
    }

    public void UpdateState(bool hasWaterConnection)
    {
        isConnected = hasWaterConnection;
        pipeRenderer.color = isConnected ? connectedColour : disconnectedColour;
    }

}

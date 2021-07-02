using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PipePuzzleManager : MonoBehaviour {

    [Header("References")]
    public Transform source;
    public Pipe target;
    public Pipe[] pipes;
    public ItemSO rewardItem;
    public LayerMask pipeLayer;

    [Header("Events")]
    public UnityEvent puzzleCompletionEvent;

    public void UpdateConnections()
    {
        // Perform a recursion through the pipes that are connected to the master
        List<Pipe> connectedPipes = new List<Pipe>();

        // Update connections for all pipes in the network
        foreach(Pipe pipe in pipes)
        {
            pipe.UpdateConnections();
        }

        // Check for a first connection to the master
        RaycastHit2D hit = Physics2D.Raycast(source.position, Vector3.right, 0.1f, pipeLayer);
        Pipe connectedPipe;
        if(hit && (connectedPipe = hit.transform.GetComponent<Pipe>()) != null)
        {
            connectedPipes.Add(connectedPipe);

            // Check which pipes are connected to the master (using SortedSet to avoid duplicates)
            // Only needs doing if there is an initial pipe connected to the source
            AddConnection(connectedPipe, ref connectedPipes);
        }

        // Update the state of all pipes in the system based on connections
        foreach(Pipe pipe in pipes)
        {
            if(connectedPipes.Contains(pipe))
            {
                pipe.UpdateState(true);
            } else
            {
                pipe.UpdateState(false);
            }
        }

        // Check for win condition
        if(target.isConnected)
        {
            // The puzzle is complete
            Debug.Log("Puzzle Complete");
            //Player.Instance.playerInventory.AddItem(rewardItem);
            puzzleCompletionEvent?.Invoke();
        }

    }

    public void AddConnection(Pipe pipeToCheck, ref List<Pipe> connectedPipes)
    {
        // Check if there ISN'T a connection to another pipe
        List<Pipe> unregisteredConnections = new List<Pipe>();
        foreach(Pipe connection in pipeToCheck.connections)
        {
            if(!connectedPipes.Contains(connection))
            {
                unregisteredConnections.Add(connection);
            }
        }

        if(unregisteredConnections.Count == 0)
        {
            // Exit condition
            return;
        }

        // If there were unregistered connections, repeat the process for each one
        foreach(Pipe pipe in unregisteredConnections)
        {
            connectedPipes.Add(pipe);
            AddConnection(pipe, ref connectedPipes);
        }
    }
}

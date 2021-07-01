using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WireManager : MonoBehaviour
{
    static public WireManager Instance;

    public int switchCount;
    private int onCount = 0;
    public UnityEvent onCompletionEvent = new UnityEvent();

    public Transform[] draggableWires;
    public Transform[] socketWires;

    private void Start()
    {
        Instance = this;
        RandomizeWirePositions(draggableWires);
        RandomizeWirePositions(socketWires);
    }

    public void SwitchChange(int points)
    {
        onCount = onCount + points;
        if (onCount == switchCount)
        {
            Debug.Log("Puzzle Completed");
            onCompletionEvent?.Invoke();
        }
    }

    public void RandomizeWirePositions(Transform[] wireTransforms) {
        for (int i = 0; i < wireTransforms.Length; i++)
        {
            int swapIndex = Random.Range(0, wireTransforms.Length);
            Vector3 temp = wireTransforms[i].position;
            wireTransforms[i].position = wireTransforms[swapIndex].position;
            wireTransforms[swapIndex].position = temp;
        }
    }
}

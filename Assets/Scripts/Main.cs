using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Main : MonoBehaviour
{
    static public Main Instance;

    public int switchCount;
    private int onCount = 0;
    public UnityEvent onCompletionEvent = new UnityEvent();

    private void Awake()
    {
        Instance = this;
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
}

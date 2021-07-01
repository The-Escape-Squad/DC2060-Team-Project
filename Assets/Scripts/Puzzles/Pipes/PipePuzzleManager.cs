using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipePuzzleManager : MonoBehaviour
{
    public GameObject PipesHolder;
    public GameObject[] Pipes;
    public ItemSO rewardItem;

    [SerializeField]
    int totalPipes = 0;
    [SerializeField]
    int correctedPipes = 0;

    // Start is called before the first frame update
    void Start()
    {
        totalPipes = PipesHolder.transform.childCount;

        Pipes = new GameObject[totalPipes];

        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    public void correctMove()
    {
        correctedPipes += 1;

        if (correctedPipes == totalPipes)
        {
            Player.Instance.playerInventory.AddItem(rewardItem);
        }
    }

    public void wrongMove()
    {
        correctedPipes -= 1;
    }
}

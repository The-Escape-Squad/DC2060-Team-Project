using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeRotator : MonoBehaviour
{

    public PipePuzzleManager manager;

    public void OnMouseDown()
    {
        transform.Rotate(Vector3.forward * 90);
        StartCoroutine(WaitToUpdate());
    }
    public IEnumerator WaitToUpdate()
    {
        yield return new WaitForSeconds(0.075f);
        manager.UpdateConnections();
    }

}

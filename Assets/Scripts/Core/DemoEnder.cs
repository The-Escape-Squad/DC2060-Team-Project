using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoEnder : MonoBehaviour
{

    [SerializeField] private Animator fadeAnimator;

    public void EndDemo()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(2);
        fadeAnimator.SetTrigger("Fade");
        Destroy(Player.Instance.gameObject);
    }

}

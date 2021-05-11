using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockAnimator : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    
    public void OnMouseDown()
    {
        animator.SetTrigger("LockFade");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}

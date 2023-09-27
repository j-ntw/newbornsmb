using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickController : MonoBehaviour
{
    public Animator brickAnimator;
    public Animator coinAnimator;

    void OnTriggerEnter2D(Collider2D other)
    {
        // self is question, other is mario
        Debug.Log("Hit brick");

        brickAnimator.SetTrigger("jumpTrigger");
        coinAnimator.SetTrigger("jumpTrigger");
    }
}
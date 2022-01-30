using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectOnAnimation : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Empty"))
            Destroy(gameObject);
    }
}

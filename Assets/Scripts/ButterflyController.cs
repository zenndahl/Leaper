using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyController : Target
{
    protected Animator animator;
    protected int butterflyType = 2;

    private void Start()
    {
        animator = GetComponent<Animator>();

        SetButterflyType();

        if (butterflyType == 2) animator.SetBool("Butterfly 2", true);
        if (butterflyType == 3) animator.SetBool("Butterfly 3", true);
        if (butterflyType == 4) animator.SetBool("Butterfly 4", true);
    }

    protected virtual void SetButterflyType()
    {
        butterflyType = Random.Range(1, 2);
    }
}

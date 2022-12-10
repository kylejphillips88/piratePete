using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class BossActions : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    Rigidbody2D rb;
    Animator animator;

    private void Start()
    {
        rb = animator.GetComponent<Rigidbody2D>();
        animator = rb.GetComponent<Animator>();
        Begin();
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x > player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
    public void Begin()
    {
        animator.SetTrigger("run");
    }

}

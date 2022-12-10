using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Disappear()
    {
        anim.SetTrigger("collected");
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }

}

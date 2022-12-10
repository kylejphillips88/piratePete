using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public float attackRange = 1f;
    public LayerMask attackMask;
    public Transform attackPointRight;
    public Transform attackPointLeft;

    public void Attack()
    {
        
        Collider2D colInfo1 = Physics2D.OverlapCircle(attackPointLeft.position, attackRange, attackMask);
        Collider2D colInfo = Physics2D.OverlapCircle(attackPointRight.position, attackRange, attackMask);
            if (colInfo != null || colInfo1 != null)
            {
                colInfo.GetComponent<PlayerLife>().Die();
            }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
        Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sprite;
    [SerializeField] public AudioSource enemyDeathSoundEffect;
    public bool enemyRunning = false;


    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void EnemyRun()
    {
        WayPointFollower waypoint = gameObject.GetComponent<WayPointFollower>();
        enemyRunning = true;
        if (enemyRunning == true)
        {
            if (waypoint.currentWayPointIndex == 0)
            {
                anim.SetTrigger("run");
                sprite.flipX = false;
            }
            else { 
                anim.SetTrigger("run");
                sprite.flipX = true;
            }
        }
    }

    public void EnemyDie()
    {
        if (enemyRunning == true)
        {
            WayPointFollower wayPoint = gameObject.GetComponent<WayPointFollower>();
            wayPoint.enemyAlive = false;
            enemyDeathSoundEffect.Play();
            anim.SetTrigger("hit");
        }
        else
        {
            enemyDeathSoundEffect.Play();
            anim.SetTrigger("hit");
        }
    }

    private void EnemyDead()
    {
        Destroy(this.gameObject);
    }
}

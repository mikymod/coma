﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    protected string enemyType;
    public float healthPoint;
    protected float speed;
    protected string bulletType;
    protected bool immune;
    protected int score;

    protected bool canMove;
    protected bool canShoot;
    protected bool nextFire;
    protected float resetTime;
    protected float cooldown;

    protected float pauseStart;
    protected float pauseFinish;

    // Start is called before the first frame update
    void Awake()
    {
        canMove = true;
        canShoot = true;

        pauseStart = 0;
        pauseFinish = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void Shoot ()
    {
        if (canShoot)
        {
            if ((Time.time - (pauseFinish - pauseStart)) > resetTime)
                nextFire = true;

            if (nextFire && (Time.time - (pauseFinish - pauseStart)) > resetTime)
            {
                pauseStart = 0;
                pauseFinish = 0;

                GetComponent<Animator>().SetBool("ShootClick", true);

                resetTime = Time.time + cooldown;
            }
        }
    }

    protected void DisableShootAnimation()
    {
        GetComponent<Animator>().SetBool("ShootClick", false);
    }

    public void TakeDamage (float damage)
    {
        if (!immune)
            healthPoint -= damage;

        if (healthPoint <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Animator>().SetBool("Death", true);
        }
    }

    protected void DestroyEnemy ()
    {
        GameObject.Destroy(transform.parent.gameObject);
    }

    virtual public void PauseOn()
    {
        canShoot = false;
        canMove = false;
        pauseStart = Time.time;
        GetComponent<Animator>().speed = 0f;
    }

    virtual public void PauseOff()
    {
        canShoot = true;
        canMove = true;
        pauseFinish = Time.time;
        GetComponent<Animator>().speed = 1f;
    }

    protected void CheckBounds()
    {
        var pos = transform.position;
        if (pos.x > 2.1f || pos.x< -2.1f || pos.y> 1.4f || pos.y< -1.4f)
            GameObject.Destroy(transform.parent.gameObject);
    }
    
    public int Score
    {
        get { return score; }
    }

    public float Speed { get => speed; set => speed = value; }
    public float Cooldown { get => cooldown; set => cooldown = value; }
}

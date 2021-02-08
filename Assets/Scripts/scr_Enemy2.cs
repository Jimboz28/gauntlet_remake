using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Enemy2 : MonoBehaviour
{
    public int health = 100;
    public float speed;
    public float stoppingDistance;
    Rigidbody2D rgb2D;
    int direction = 1;

    public Transform player; 
    Animator animator;

    void Start()
    {
        rgb2D = GetComponent<Rigidbody2D> ();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);     
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
    }
        void OnTriggerExit2D(Collider2D other)
    {
        scr_EnemyAware controller = other.GetComponent<scr_EnemyAware>();
    }

    public void TakeDamage (int damage)
    {
        health -= damage;
        if (health <=0)
        {
        scr_ui u = GameObject.FindGameObjectWithTag("ui").GetComponent<scr_ui>();
        if (u!=null)
        {
        u.ChangeScore(1);
        }
            Die();
        }
        
    }
        void OnCollisionEnter2D(Collision2D other)
    {
        scr_PlayerController player = other.gameObject.GetComponent<scr_PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-50);
            Die();
        }
    }
    void Die()
    {
        // Instantiate (deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

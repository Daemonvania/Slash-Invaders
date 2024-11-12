using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BasicEnemy : EnemyCore
{

    [SerializeField] private Collider2D hitBox;
    private Rigidbody2D rb;
    [SerializeField] float knockbackForce = 1;
    private bool canMove = true;
    private bool canTakeDamage = true;
     void Start()
    {
        base.Start();
        hitBox.enabled = false;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) > stoppingDistance)
        {
            if (canMove)
            {
                Vector2 playerPos = new Vector2(player.transform.position.x, transform.position.y);
                Vector2 direction = (playerPos - rb.position).normalized;
                rb.velocity = direction * walkSpeed;
            }
        }
        else
        {
            if (canAttack)
            {
                canAttack = false;
                
                StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.2f);
        hitBox.enabled = false;
        yield return new WaitForSeconds(0.1f);
        hitBox.enabled = true;
        canAttack = true;
    }
    
    public override async void TakeDamage(int damage)
    {
        canTakeDamage = false;
        health -= damage;
        canMove = false;
        rb.velocity = Vector2.zero;
        
        Vector2 playerPos = new Vector2(player.transform.position.x, transform.position.y);
        Vector2 direction = (rb.position - playerPos).normalized; 
        rb.AddForce(direction * knockbackForce , ForceMode2D.Impulse);
        
        if (health <= 0)
        {
            Die();
        }

        await Task.Delay(100);
        canMove = true;
        canTakeDamage = true;

    }
    public override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Hit"))
        {
            TakeDamage(1);
        }
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}

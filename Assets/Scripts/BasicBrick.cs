using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasicBrick : Brick
{
    public override void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    
    public override void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            TakeDamage(1);
        }
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            TakeDamage(3);
        }
    }

    public override void Die()
    {
        if (Random.Range(0, 100) < 25)
        {
            Debug.Log("PowerUp");
            Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}

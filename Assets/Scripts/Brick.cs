using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Brick : MonoBehaviour
{
    public int health = 3;
    
    public abstract void TakeDamage(int damage);

    public abstract void OnCollisionEnter2D(Collision2D col);

    public abstract void OnTriggerEnter2D(Collider2D col);

    public abstract void Die();
}

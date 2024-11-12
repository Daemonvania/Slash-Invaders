using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCore : MonoBehaviour
{
    public int health = 3;
    
    [HideInInspector] public bool canAttack = true;
    
    [HideInInspector] public GameObject player;
    [SerializeField] public float walkSpeed = 5f;
    [SerializeField] public float stoppingDistance = 0.5f;
    
    
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public abstract void TakeDamage(int damage);

    public abstract void OnTriggerEnter2D(Collider2D col);

    public abstract void Die();
}

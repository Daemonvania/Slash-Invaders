using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 3; 
    private ManageGame manageGame;
    private int health;
    private bool canTakeDamage = true;
    [SerializeField] private float invincibilityTime = 0.5f;
    private float invincibilityTimeConverted;
    private void Start()
    {
        health = maxHealth;
        manageGame = GameObject.FindWithTag("GameManager").GetComponent<ManageGame>();
        invincibilityTimeConverted = invincibilityTime * 100;
    }
    
    public async void TakeDamage(int damage)
    {
        if (!canTakeDamage)
        {
            return;
        }

        canTakeDamage = false;
        health -= damage;
        if (health <= 0)
        {
            Die();
        }

        await Task.Delay(Mathf.RoundToInt(invincibilityTimeConverted));
        Debug.Log("CanTakeDamage");
        canTakeDamage = true;
    }

    public void Die()
    {
        manageGame.BallLost();
    }
    
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Damage"))
        {
            TakeDamage(1);
        }
    }
}

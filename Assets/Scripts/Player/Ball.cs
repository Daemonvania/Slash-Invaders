using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] float level1Speed = 5f;
    [SerializeField] float level2Speed = 5f;
    [SerializeField] float level3Speed = 5f;

    private GameObject _player;
    private int ballLevel = 1;
    private int hitAmount = 0;

    private float currentSpeed;
    
    private bool isOnPlayer = true;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (isOnPlayer)
        {
            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + 0.5f, _player.transform.position.z);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hit"))
        {
            isOnPlayer = false;
            hitAmount++;
            if (hitAmount == 3 || hitAmount == 6 || hitAmount == 9)
            {
                ballLevel++;
            }
            
            UpdateCurrentSpeed();
            Vector2 direction = (transform.position - _player.transform.position).normalized;
            // _rigidbody2D.velocity = Vector2.zero;
            // _rigidbody2D.AddForce(direction * currentSpeed, ForceMode2D.Impulse);
            _rigidbody2D.velocity = direction * currentSpeed;
        }

        if (other.CompareTag("Bottom"))
        {
            HitBottom();
        }
    }

    void HitBottom()
    {
        _rigidbody2D.velocity = Vector2.zero;
        hitAmount = 0;
        ballLevel = 1;
        isOnPlayer = true;
    }
    
    void UpdateCurrentSpeed()
    {
        switch (ballLevel)
        {
            case 1:
                currentSpeed = level1Speed;
                break;
            case 2:
                currentSpeed = level2Speed;
                break;
            case 3:
                currentSpeed = level3Speed;
                break;
            case 4:
                currentSpeed = level3Speed;
                break;
        }
    }
}

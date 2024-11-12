using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    
    [SerializeField] Collider2D fireCollider;

    [SerializeField] float level1Speed = 5f;
    [SerializeField] float level2Speed = 5f;
    [SerializeField] float level3Speed = 5f;


    private GameObject _player;
    private int hitAmount = 0;

    private float currentSpeed;
    
    private bool isOnPlayer = true;
    private bool canGrab = true;
    
    private ManageGame _manageGame;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _player = GameObject.FindWithTag("Player");
        _manageGame = GameObject.FindWithTag("GameManager").GetComponent<ManageGame>();
        
        // fireCollider.enabled = false;
    }

    private void Update()
    {
        if (isOnPlayer)
        {
            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + 0.5f, _player.transform.position.z);
        }
    }

    // Update is called once per frame
    private async void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hit"))
        {
            isOnPlayer = false;
            canGrab = false;
            hitAmount++;
            if (hitAmount == 3 || hitAmount == 6 || hitAmount == 9)
            {
                _manageGame.IncreaseBallLevel();
            }
            
            UpdateCurrentSpeed();
            Vector2 direction = (transform.position - _player.transform.position).normalized;
            // _rigidbody2D.velocity = Vector2.zero;
            // _rigidbody2D.AddForce(direction * currentSpeed, ForceMode2D.Impulse);
            _rigidbody2D.velocity = direction * currentSpeed;
            
            
            await Task.Delay(50);
            canGrab = true;
        }

        if (other.CompareTag("Bottom"))
        {
            _manageGame.BallLost();
            ReturnToPlayer();
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (canGrab && other.CompareTag("Grab"))
        {
            ReturnToPlayer();
        }
    }

    void ReturnToPlayer()
    {
        _rigidbody2D.velocity = Vector2.zero;
        hitAmount = 0;
       _manageGame.ballLevel = 1;
       fireCollider.enabled = false;
        isOnPlayer = true;
    }
    
    void UpdateCurrentSpeed()
    {
        switch (_manageGame.ballLevel)
        {
            case 1:
                currentSpeed = level1Speed;
                break;
            case 2:
                currentSpeed = level2Speed;
                break;
            case 3:
                currentSpeed = level3Speed;
                fireCollider.enabled = true;
                break;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInput _playerInput;
    private CharacterController _characterController;
    private PlayerInputActions playerInputActions;


    private float moveSpeed;
    [SerializeField] float level1Movespeed = 5f;
    [SerializeField] float level2Movespeed = 8f;
    [SerializeField] float level3Movespeed = 11f;
    
    bool canHit = true;

    
    [SerializeField] GameObject hitBox;
    
    private ManageGame _manageGame;
    
    // Start is called before the first frame update    
    void Awake()
    {
        hitBox.SetActive(false);
        _playerInput = GetComponent<PlayerInput>();
        _characterController = GetComponent<CharacterController>();
        playerInputActions =  new PlayerInputActions();
        playerInputActions.Enable();
        playerInputActions.Player.Fire.performed += Hit;
        
        moveSpeed = level1Movespeed;
        _manageGame = GameObject.FindWithTag("GameManager").GetComponent<ManageGame>();
      
    }
    void Update()
    {
        Vector2 moveInput = playerInputActions.Player.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(moveInput.x * moveSpeed * Time.deltaTime, 0, 0);
        _characterController.Move(move);
        _characterController.gameObject.transform.position = new Vector3(Mathf.Clamp(_characterController.gameObject.transform.position.x, -6.5f, 6.5f), _characterController.gameObject.transform.position.y, _characterController.gameObject.transform.position.z);
    }

    private async void Hit(InputAction.CallbackContext context)
    {
        if (!canHit) return;
        canHit = false;
        hitBox.SetActive(true);
        await Task.Delay(250);
        hitBox.SetActive(false);
        await Task.Delay(250);
        canHit = true;
    }
    
    public void IncreaseSpeed()
    {
        switch (_manageGame.ballLevel)
        {
            case 2:
                moveSpeed = level2Movespeed;
                break;
            case 3:
                moveSpeed = level3Movespeed;
                break;
        }
    }

    public void ResetSpeed()
    {
        moveSpeed = level1Movespeed;
    }
    
    // Update is called once per frame
   
}
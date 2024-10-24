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
    
    
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject hitBox;
    
    bool canHit = true;
    // Start is called before the first frame update    
    void Awake()
    {
        hitBox.SetActive(false);
        _playerInput = GetComponent<PlayerInput>();
        _characterController = GetComponent<CharacterController>();
        playerInputActions =  new PlayerInputActions();
        playerInputActions.Enable();
        playerInputActions.Player.Fire.performed += Hit;
      
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

    
    // Update is called once per frame
   
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IObserver {

	public CharacterController2D controller;
	public Animator animator;
	[SerializeField] Subject _ManagerSubject;
	
	private float currentSpeed;
	
	public float level1Speed = 40f;
	public float level2Speed = 60f;
	public float level3Speed = 80f;

	PlayerInputActions playerInputActions;
	
	float horizontalMove = 0f;
	bool jump = false;
	bool dash = false;

	//bool dashAxis = false;

	private void OnEnable()
	{
		_ManagerSubject.AddObserver(this);
	}
    
	private void OnDisable()
	{
		_ManagerSubject.RemoveObserver(this);
	}
	
	private void Awake()
	{
		playerInputActions = new PlayerInputActions();
		playerInputActions.Enable();
		currentSpeed = level1Speed;
		// playerInputActions.Player.Jump.performed += ctx => jump = true;
		// playerInputActions.Player.Dash.performed += ctx => dash = true;
	}

	// Update is called once per frame
	void Update () {

		horizontalMove = playerInputActions.Player.Move.ReadValue<Vector2>().x * currentSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (playerInputActions.Player.Jump.triggered)
		{
			jump = true;
		}

		if (playerInputActions.Player.Dash.triggered)
		{
			dash = true;
		}

		/*if (Input.GetAxisRaw("Dash") == 1 || Input.GetAxisRaw("Dash") == -1) //RT in Unity 2017 = -1, RT in Unity 2019 = 1
		{
			if (dashAxis == false)
			{
				dashAxis = true;
				dash = true;
			}
		}
		else
		{
			dashAxis = false;
		}
		*/
	}

	public void OnFall()
	{
		animator.SetBool("IsJumping", true);
	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash);
		jump = false;
		dash = false;
	}
	
	public void OnNotify(GameActions action, ManageGame game)
	{
		if (action == GameActions.BallLevelUp || action == GameActions.BallLost)
		{
			switch (game.ballLevel)	
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
				default:
					break;
			}
		}
		Debug.Log("PlayerMovement:" + action);
	}
}

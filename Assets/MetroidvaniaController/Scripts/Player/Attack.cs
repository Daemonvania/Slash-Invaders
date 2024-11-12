using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
	public float dmgValue = 4;
	public GameObject throwableObject;
	public Transform attackCheck;
	private Rigidbody2D m_Rigidbody2D;
	public Animator animator;
	public bool canAttack = true;
	public bool isTimeToCheck = false;

	public GameObject cam;

	 Collider2D currentHitBox;
	[SerializeField] Collider2D hitBoxUp;
	[SerializeField] Collider2D hitBoxDown;
	[SerializeField] Collider2D hitBoxDefault;
	
	
	[SerializeField] PlayerInputActions playerInputActions;
	
	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		playerInputActions = new PlayerInputActions();
		playerInputActions.Enable();
		playerInputActions.Player.Fire.performed += Hit;
		
		hitBoxDefault.enabled = false;
		hitBoxUp.enabled = false;
		hitBoxDown.enabled = false;
		currentHitBox = hitBoxDefault;
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

	 //    if (Input.GetKeyDown(KeyCode.V))
		// {
		// 	GameObject throwableWeapon = Instantiate(throwableObject, transform.position + new Vector3(transform.localScale.x * 0.5f,-0.2f), Quaternion.identity) as GameObject; 
		// 	Vector2 direction = new Vector2(transform.localScale.x, 0);
		// 	throwableWeapon.GetComponent<ThrowableWeapon>().direction = direction; 
		// 	throwableWeapon.name = "ThrowableWeapon";
		// }
	}
    private async void Hit(InputAction.CallbackContext context)
    {
	    if (!canAttack) return;
	    animator.SetTrigger("Attack");


	    if (playerInputActions.Player.Move.ReadValue<Vector2>().y > 0)
	    {
		    animator.SetTrigger("AttackUp");
		    currentHitBox = hitBoxUp;
	    }
	    else if (playerInputActions.Player.Move.ReadValue<Vector2>().y < 0)
	    {
		    animator.SetTrigger("AttackDown");
		    currentHitBox = hitBoxDown;
	    }
	    canAttack = false; 
	    currentHitBox.enabled = true;
	    Debug.Log(currentHitBox);
	    await Task.Delay(250);
	    currentHitBox.enabled = false;
	    currentHitBox = hitBoxDefault;
	    
	    await Task.Delay(250);
	    canAttack = true;
    }

    public void DoDashDamage()
	{
		dmgValue = Mathf.Abs(dmgValue);
		Collider2D[] collidersEnemies = Physics2D.OverlapCircleAll(attackCheck.position, 0.9f);
		for (int i = 0; i < collidersEnemies.Length; i++)
		{
			if (collidersEnemies[i].gameObject.tag == "Enemy")
			{
				if (collidersEnemies[i].transform.position.x - transform.position.x < 0)
				{
					dmgValue = -dmgValue;
				}
				collidersEnemies[i].gameObject.SendMessage("ApplyDamage", dmgValue);
				cam.GetComponent<CameraFollow>().ShakeCamera();
			}
		}
	}
}

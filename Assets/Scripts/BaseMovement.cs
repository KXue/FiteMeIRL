using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class BaseMovement : MonoBehaviour{
	#region Public Variables
	public float m_gravity = 20.0f;
	public float m_maxSpeed = 7.5f;
	public float m_jumpSpeed = 8.0f;
	public float m_sprintSpeed = 10.0f;
	#endregion
	#region Private Variables
	private float m_sprintFactor;
	private bool m_jump = false;
	private bool m_sprint = false;
	private bool m_grounded = true;
	private Vector3 m_moveDirection;
	private Animator m_animationController;
	private CharacterController m_characterController;
	#endregion
	// Use this for initialization
	void Start () {
		m_sprintFactor = m_sprintSpeed / m_maxSpeed;
		m_animationController = GetComponent<Animator>();
		m_characterController = GetComponent<CharacterController>();
		m_moveDirection = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		// if(isLocalPlayer){
			Vector2 movementXZ = MovementXZ();
			m_moveDirection.x = movementXZ.x;
			m_moveDirection.z = movementXZ.y;
			m_moveDirection.y = MovementY(m_moveDirection.y);
			m_grounded = (m_characterController.Move(m_moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
		// }
	}
	Vector2 MovementXZ(){
		Vector2 inputXZ = HandleXZInput();
		inputXZ = Sprint(inputXZ, ref m_sprint);
		UpdateAnimationXZ(inputXZ);
		return inputXZ * m_maxSpeed;
	}
	Vector2 HandleXZInput(){
		Vector2 retVal =  new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		
		if(retVal.magnitude > 1){
			retVal.Normalize();
		}

		return retVal;
	}
	Vector2 Sprint(Vector2 inputXZ, ref bool isSprinting){
		if(m_grounded && Input.GetButtonDown("Sprint")){
			isSprinting = true;
		} 
		else if(Input.GetButtonUp("Sprint") || Input.GetAxis("Vertical") < 0){
			isSprinting = false;
		}
		if(isSprinting){
			inputXZ.y = m_sprintFactor;
		}
		return inputXZ;
	}
	void UpdateAnimationXZ(Vector2 inputXZ){
		m_animationController.SetFloat("Right", inputXZ.x);
		m_animationController.SetFloat("Forward", inputXZ.y);
	}
	float MovementY(float currentMovementY){
		if(m_grounded && Input.GetButtonDown("Jump")){
			m_animationController.SetTrigger("Jump");
			currentMovementY = m_jumpSpeed;
		}
		else if(!m_grounded){
			currentMovementY -= m_gravity * Time.deltaTime;
		}
		return currentMovementY;
	}
}

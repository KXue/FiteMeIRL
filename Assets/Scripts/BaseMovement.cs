using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class BaseMovement : NetworkBehaviour{
	#region Public Variables
	public float m_gravity;
	public float m_maxSpeed;
	public float m_jumpSpeed;
	public float m_sprintSpeed;
	#endregion
	#region Private Variables
	private bool m_jump = false;
	private bool m_grounded = true;
	private Vector3 m_moveDirection;
	private Animator m_animationController;
	private CharacterController m_characterController;
	#endregion
	// Use this for initialization
	void Start () {
		m_animationController = GetComponent<Animator>();
		m_characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		m_moveDirection = Vector3.zero;

		m_moveDirection.z = Input.GetAxis("Vertical");
		m_moveDirection.x = Input.GetAxis("Horizontal");

		if(m_moveDirection.magnitude > 1){
			m_moveDirection.Normalize();
		}
		m_jump = Input.GetButtonDown("Jump");

		// m_characterController.Move(m_moveDirection);
		m_animationController.SetFloat("Right", m_moveDirection.x);
		m_animationController.SetFloat("Forward", m_moveDirection.z);
		m_animationController.SetFloat("Speed", m_moveDirection.magnitude);
		if(m_jump){
			m_animationController.SetBool("Jump", m_jump);
			m_jump = false;
		}
	}
	// Vector2 VelocityXZ(){
	// 	Vector2 retVal =  new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
	// 	if(retVal.magnitude > 1){
	// 		retVal.Normalize();
	// 	}
	// 	if(Input.GetButton("Sprint")){

	// 	}
	// 	return retVal;

	// }
	// float VelocityY(float currentMovementY, bool isGrounded, out bool isJumping){
	// 	isJumping = false;
	// 	if(isGrounded && Input.GetButtonDown("Jump")){
	// 		isJumping = true;
	// 		currentMovementY = m_jumpSpeed;
	// 	}
	// 	else if(!isGrounded){
	// 		currentMovementY -= m_gravity * Time.deltaTime;
	// 	}
	// 	return currentMovementY;
	// }
}

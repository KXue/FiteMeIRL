using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour {
	#region Public Variables
	public float m_maxSpeed;
	public float m_sprintSpeed;
	private Vector3 m_moveDirection;
	private Animator m_animationController;
	private CharacterController m_characterController;
	[SerializeField]
	private bool m_jump = false;
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
}

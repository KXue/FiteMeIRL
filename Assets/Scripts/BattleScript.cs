using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScript : MonoBehaviour {

	private Animator m_animator;
	private bool m_isAttacking = false;

	void Start() {
		m_animator = GetComponent<Animator>();
	}

	void Update() {
		if(Input.GetButtonDown("Attack")) {
			m_animator.SetTrigger("isAttacking");
		}
	}
}

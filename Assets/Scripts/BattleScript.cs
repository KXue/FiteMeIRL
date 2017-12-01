using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScript : MonoBehaviour {

	public Transform prefab;
	public GameObject prefabSpawnPoint;

	private Animator m_animator;
	private bool m_isAttacking = false;

	void Start() {
		m_animator = GetComponent<Animator>();
	}

	void Update() {
		if(Input.GetButtonDown("Attack") && !m_isAttacking) {
			m_animator.SetTrigger("isAttacking");
			m_isAttacking = true;
		}
	}

	public void SpawnAttack() {
		var fireBall = Instantiate(prefab, prefabSpawnPoint.transform.position, transform.rotation);
		fireBall.GetComponent<Rigidbody>().velocity = fireBall.transform.forward * 6;

		Destroy(fireBall.gameObject, 2.0f);
	}

	public void EndOfAttack() {
		m_isAttacking = false;
	}
}

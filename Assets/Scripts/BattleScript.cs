using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScript : MonoBehaviour {

	public Transform prefab;
	public GameObject prefabSpawnPoint;
	public float m_fireBallSpeed = 30.0f;
	private bool m_isAttacking = false;

	private Animator m_animator;

	void Start() {
		m_animator = GetComponent<Animator>();
	}

	void Update() {
		if(Input.GetButton("Fire2") && !m_isAttacking) {
			m_animator.SetTrigger("isAttacking");
			m_isAttacking = true;
		}
	}

	public void SpawnAttack() {
		var fireBall = Instantiate(prefab, prefabSpawnPoint.transform.position, AimAtCrosshair());
		fireBall.GetComponent<Rigidbody>().velocity = fireBall.transform.forward * m_fireBallSpeed;

		Destroy(fireBall.gameObject, 2.0f);
	}
	public void EndAttack(){
		m_isAttacking = false;
	}

	Quaternion AimAtCrosshair(){
		Camera thirdPersonCamera = Camera.main;
        Quaternion retVal = thirdPersonCamera.transform.rotation;
        RaycastHit hitInfo;
		Ray aimRay = thirdPersonCamera.ScreenPointToRay(new Vector3(thirdPersonCamera.pixelWidth * 0.5f, thirdPersonCamera.pixelHeight * 0.5f, 0));

		bool didHit = Physics.Raycast(
			aimRay.origin, 
			aimRay.direction, 
			out hitInfo, 
			Mathf.Infinity, 
			LayerMask.NameToLayer("Ground") | LayerMask.NameToLayer("Enemy"));
		if(didHit){
			retVal = Quaternion.LookRotation(hitInfo.point - prefabSpawnPoint.transform.position);
		}
		return retVal;
	}
}

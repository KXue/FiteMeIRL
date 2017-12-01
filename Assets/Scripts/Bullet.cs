using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private GameObject m_owner;
	public void SetOwner(GameObject owner){
		m_owner = owner;
	}
	void OnTriggerEnter(Collider other)
	{
		GameObject hit = other.gameObject;
		if(hit != m_owner){
			Health health = hit.GetComponent<Health>();
			if(health != null){
				health.TakeDamage(10);
			}
			Destroy(gameObject);
		}
	}
}

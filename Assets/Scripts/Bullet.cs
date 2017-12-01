using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour {

	/// <summary>
	/// OnCollisionEnter is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionEnter(Collision other)
	{
		GameObject hit = other.gameObject;
		Health health = hit.GetComponent<Health>();
		if(health != null){
			health.TakeDamage(10);
		}
		Destroy(gameObject);
	}
}

using UnityEngine;
using UnityEngine.Networking;


public class PlayerController : NetworkBehaviour
{
	public float m_bulletSpeed = 6f;
	public Transform m_bulletPrefab;
	public Transform m_bulletSpawn;
	void Update()
	{
		if(isLocalPlayer){
			float x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
			float z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

			transform.Rotate(0, x, 0);
			transform.Translate(0, 0, z);

			if(Input.GetButtonDown("Fire1")){
				CmdFire();
			}
		}
	}
	[Command]
	void CmdFire(){
		GameObject bullet = Instantiate(m_bulletPrefab, m_bulletSpawn.position, m_bulletSpawn.rotation).gameObject;
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * m_bulletSpeed;;
		
		NetworkServer.Spawn(bullet);

		Destroy(bullet, 2.0f);
	}
	public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
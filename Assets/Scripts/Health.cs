using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
	public const int m_maxHealth = 100;
	public RectTransform m_healthBar;
	public bool m_destroyOnDeath;
    [SyncVar(hook = "OnChangeHealth")]
	private int m_currentHealth = m_maxHealth;
	private NetworkStartPosition[] m_spawnPoints;
	void Start(){
		if(isLocalPlayer){
			m_spawnPoints = FindObjectsOfType<NetworkStartPosition>();
		}
	}
	public int CurrentHealth{
		get{
			return m_currentHealth;
		}
	}
	public void TakeDamage(int amount){
		if(isServer){
			m_currentHealth -= amount;
			if(m_currentHealth <= 0){
				if(m_destroyOnDeath){
					Destroy(gameObject);
				}
				else{
					m_currentHealth = m_maxHealth;
					RpcRespawn();
				}
			}
		}
	}
	void OnChangeHealth (int health)
    {
        m_healthBar.sizeDelta = new Vector2(health, m_healthBar.sizeDelta.y);
    }
	[ClientRpc]
	void RpcRespawn()
	{
		if (isLocalPlayer)
		{
			Vector3 spawnPoint = Vector3.zero;

			if(m_spawnPoints != null && m_spawnPoints.Length > 0){
				spawnPoint = m_spawnPoints[Random.Range(0, m_spawnPoints.Length)].transform.position;
			}

			transform.position = spawnPoint;
		}
	}
}

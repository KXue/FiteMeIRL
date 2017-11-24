using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
	public const int m_maxHealth = 100;
	public RectTransform m_healthBar;
    [SyncVar(hook = "OnChangeHealth")]
	private int m_currentHealth = m_maxHealth;
	public int CurrentHealth{
		get{
			return m_currentHealth;
		}
	}
	public void TakeDamage(int amount){
		if(isServer){
			m_currentHealth -= amount;
			if(m_currentHealth <= 0){
				m_currentHealth = m_maxHealth;
				RpcRespawn();
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
			transform.position = Vector3.zero;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	public const int MAX_HEALTH = 100;
	public float m_currentHealth = 100.0f;
	public Text m_hpText;
	public Slider m_hp;

	private bool m_isDead = false;
	

	void Start() {
		m_currentHealth = MAX_HEALTH;
		m_hpText.text = m_currentHealth + "/" + MAX_HEALTH;
		m_hp.value = m_currentHealth/MAX_HEALTH;
	}


	public void takeDamage(int amount) {
		m_currentHealth -= amount;
		CheckIsDead();
		if(!m_isDead) {
			m_hpText.text = m_currentHealth + "/" + MAX_HEALTH;
			m_hp.value = m_currentHealth/MAX_HEALTH;
		}

	}

	private bool CheckIsDead() {
		if(m_currentHealth <= 0 && !m_isDead) {
			m_currentHealth = 0;
			m_hpText.text = m_currentHealth + "/" + MAX_HEALTH;
			m_hp.value = m_currentHealth/MAX_HEALTH;
			m_isDead = true;
		}

		return m_isDead;
	}

}

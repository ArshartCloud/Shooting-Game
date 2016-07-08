using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : MonoBehaviour
{

	public const int maxHealth = 100;

	public bool destroyOnDeath;

	public int currentHealth = maxHealth;

	public RectTransform healthBar;

	public void TakeDamage (int amount)
	{
        
		currentHealth -= amount;
		OnChangeHealth (currentHealth);
		if (currentHealth <= 0) {
			if (destroyOnDeath) {
				Destroy (gameObject);
			}
			if (gameObject.tag == "Player") {
				GetComponent<Animator> ().SetBool ("Die", true);
			}
		}
	}

	void OnChangeHealth (int currentHealth)
	{
		healthBar.sizeDelta = new Vector2 (currentHealth, healthBar.sizeDelta.y);
	}
}
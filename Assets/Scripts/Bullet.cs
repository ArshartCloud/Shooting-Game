using UnityEngine;
using System.Collections;

public enum BulletType
{
	Normal,
	Grenade,
	Anaesthetic
}

public class Bullet : MonoBehaviour
{
	public BulletType bulletType;
	public float lifetime = 10.0f;

	public GameObject GrenadeExplosion;

	void Start ()
	{
		Destroy (gameObject, lifetime);
	}

	void OnCollisionEnter (Collision collision)
	{
		GameObject hit = collision.gameObject;
		if (bulletType == BulletType.Normal) {
			Health health = hit.GetComponent<Health> ();
			if (health != null) {
				health.TakeDamage (10);
				Destroy (gameObject);
			} else if (hit.tag == "Finish") {
				Destroy (gameObject);
			}	
		} else if (bulletType == BulletType.Anaesthetic) {
			Health health = hit.GetComponent<Health> ();
			if (health != null) {
				health.TakeDamage (5);
				hit.GetComponent<EnemyAI> ().Anaesthetic (3.0f);
				Destroy (gameObject);
			} else if (hit.tag == "Finish") {
				Destroy (gameObject);
			}
		} else if (bulletType == BulletType.Grenade) {
			Health health = hit.GetComponent<Health> ();
			if (health != null) {
				health.TakeDamage (30);
				Destroy (gameObject);
				Instantiate (GrenadeExplosion, transform.position, transform.rotation);
			} else if (hit.tag == "Finish") {
				gameObject.AddComponent<Boom> ();
				Instantiate (GrenadeExplosion, transform.position, transform.rotation);
				Destroy (this);
			}
		}
	}

	public void SetType (BulletType type)
	{
		bulletType = type;
		if (type == BulletType.Grenade) {
			GetComponent<Rigidbody> ().useGravity = true;
		}
	}
}

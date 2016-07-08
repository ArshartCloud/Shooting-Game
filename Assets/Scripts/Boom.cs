using UnityEngine;
using System.Collections;

public class Boom : MonoBehaviour
{

	public float lifeTime = 0.5f;
	public float lastTime;

	// Use this for initialization
	void Start ()
	{
		Destroy (GetComponent<Rigidbody> ());
		lastTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time - lastTime < lifeTime) {
			GetComponent<SphereCollider> ().radius = Mathf.Clamp (GetComponent<SphereCollider> ().radius + 0.5f, 0, 4.0f);
		} else {
			Destroy (gameObject);
		}
	}

	void OnCollisionStay (Collision collision)
	{
		GameObject hit = collision.gameObject;
		Health health = hit.GetComponent<Health> ();
		if (health != null) {
			health.TakeDamage (20);
			Destroy (gameObject);
		}
	}
}
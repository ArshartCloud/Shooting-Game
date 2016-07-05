using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{

	private bool unShooted = true;

	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "Finish") {
			if (unShooted) {
				unShooted = false;
				GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().AddScore (10);
			}
		}
	}
}

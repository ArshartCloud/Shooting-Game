using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<Animator> ().SetBool ("Die", true);
		}
	}
}

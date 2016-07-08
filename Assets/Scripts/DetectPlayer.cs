using UnityEngine;
using System.Collections;

public class DetectPlayer : MonoBehaviour
{
	public GameObject parent;

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player") {
			parent.GetComponent<EnemyAI> ().AttackTarget (other.gameObject);
//			Debug.Log ("I have got you in my sight");
		}
	}
}

using UnityEngine;
using System.Collections;

public enum Strategy
{
	Wander,
	Attack
}

public class EnemyAI : MonoBehaviour
{
	public Strategy strategy;
	private float lastTime;
	public float wanderInterval = 2f;
	public float attackInterval = 1f;
	private GameObject target;
	private NavMeshAgent agent;
	public GameObject bullet;
	public GameObject spawm;

	public bool anaesthetization = false;
	private float anaestheticTime;
	private float anaestheticStartTime;

	void Start ()
	{
		lastTime = Time.time;
		agent = GetComponent<NavMeshAgent> ();
		strategy = Strategy.Wander;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time - lastTime > attackInterval) {
			if (strategy == Strategy.Attack && !anaesthetization) {
				lastTime = Time.time;
				if (target == null) {
					strategy = Strategy.Wander;
				} else {
					agent.SetDestination (target.transform.position);
				}
				transform.LookAt (target.transform.position);
				transform.position += transform.right * Random.Range (-0.10f, 0.10f);
				GameObject myBullet = Instantiate (bullet, spawm.transform.position, 
					                      spawm.transform.rotation * Quaternion.Euler (Vector3.one + Random.insideUnitSphere * 0.1f)) as GameObject;
				myBullet.transform.LookAt (target.transform);
//				myBullet.transform.Rotate (myBullet.transform.right * -20);
			}
		}
		if (Time.time - lastTime > wanderInterval) {
			if (strategy == Strategy.Wander) {
				lastTime = Time.time;
				Vector2 targetVec2 = Random.insideUnitCircle * 5;
				agent.SetDestination (new Vector3 (targetVec2.x + transform.position.x,
					transform.position.y, targetVec2.y + transform.position.z));
			}
		}
		if (anaesthetization) {
			if (Time.time - anaestheticStartTime > anaestheticTime) {
				anaesthetization = false;
			}
		}
	}

	public void AttackTarget (GameObject attackTarget)
	{
		target = attackTarget;
		strategy = Strategy.Attack;
	}

	public void Anaesthetic (float time)
	{
		anaesthetization = true;
		anaestheticStartTime = Time.time;
		anaestheticTime = time;
	}
		
}

using UnityEngine;
using System.Collections;

public class Bazooka : MonoBehaviour
{

	public Animator animator;
	public GameObject targetA = null;
	public GameObject leftHandle = null;
	public GameObject rightHandle = null;
	public GameObject bazoo = null;
	public GameObject bullet = null;
	public GameObject spawm = null;

	private bool load = true;

	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (animator.GetBool ("Die"))
			return;
		if (animator && GetComponent<PlayerController> ().mode == PlayMode.play) {
			animator.SetFloat ("Aim", load ? 1 : 0, .1f, Time.deltaTime);

			float aim = animator.GetFloat ("Aim");
			float fire = animator.GetFloat ("Fire");

			if (Input.GetButton ("Fire1") && fire < 0.01 && aim > 0.99) {
				animator.SetFloat ("Fire", 1);

				if (bullet != null && spawm != null) {
					GameObject newBullet = (GameObject)Instantiate (bullet, spawm.transform.position, spawm.transform.rotation);
					newBullet.GetComponent<Bullet> ().SetType (gameObject.GetComponent<PlayerController> ().bulletType);
				}
			} else {
				animator.SetFloat ("Fire", 0, 0.1f, Time.deltaTime);
			}

 
//			if (Input.GetButton ("Fire2")) {
//				if (load && aim > 0.99) {
//					load = false;
//				} else if (!load && aim < 0.01)
//					load = true;
//			}
				
		}   		  
	}

	void OnAnimatorIK (int layerIndex)
	{
		float aim = animator.GetFloat ("Aim");

		// solve lookat and update bazooka transform on first il layer
		if (layerIndex == 0) {
			if (targetA != null) {
				Vector3 target = targetA.transform.position;

				target.y = target.y + 0.2f * (target - animator.rootPosition).magnitude;

				animator.SetLookAtPosition (target);
				animator.SetLookAtWeight (aim, 0.5f, 0.5f, 0.0f, 0.5f);

				if (bazoo != null) {
					float fire = animator.GetFloat ("Fire");
					Vector3 pos = new Vector3 (0.195f, -0.0557f, -0.155f);
					Vector3 scale = new Vector3 (0.2f, 0.8f, 0.2f);
					pos.x -= fire * 0.2f;
					scale = scale * aim;
					bazoo.transform.localScale = scale;
					bazoo.transform.localPosition = pos;
				}        
 
			}
		}

		// solve hands holding bazooka on second ik layer
		if (layerIndex == 1) {
			if (leftHandle != null) {
				animator.SetIKPosition (AvatarIKGoal.LeftHand, leftHandle.transform.position);
				animator.SetIKRotation (AvatarIKGoal.LeftHand, leftHandle.transform.rotation);
				animator.SetIKPositionWeight (AvatarIKGoal.LeftHand, aim);
				animator.SetIKRotationWeight (AvatarIKGoal.LeftHand, aim);
			}

			if (rightHandle != null) {
				animator.SetIKPosition (AvatarIKGoal.RightHand, rightHandle.transform.position);
				animator.SetIKRotation (AvatarIKGoal.RightHand, rightHandle.transform.rotation);
				animator.SetIKPositionWeight (AvatarIKGoal.RightHand, aim);
				animator.SetIKRotationWeight (AvatarIKGoal.RightHand, aim);
			}
		}
	}


}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public enum PlayMode
{
	play,
	view
}

public class PlayerController : MonoBehaviour
{
	public int score = 0;
	private int maxScore;
	private Camera camera;
	public GameObject shot;
	public Transform shotSpawn;
	public float speed = 0.01f;
	public float fireRate;

	protected Animator animator;
	public float DirectionDampTime = .25f;

	public Text scoreText;
	public PlayMode mode = PlayMode.play;

	public BulletType bulletType;
	public Text bulletText;

	void Start ()
	{
		camera = Camera.main;
		camera.transform.parent = transform;
		camera.transform.localPosition = new Vector3 (0.91f, 1.65f, -1.73f);

		animator = GetComponent<Animator> ();

		Cursor.lockState = CursorLockMode.Locked;

		if (!PlayerPrefs.HasKey ("MaxScore"))
			PlayerPrefs.SetInt ("MaxScore", 0);
		maxScore = PlayerPrefs.GetInt ("MaxScore");

		scoreText.text = "Score: 0\nMax Score: " + maxScore;

		ChangeBullet (BulletType.Normal);

	}

	void Update ()
	{
		if (mode == PlayMode.play) {
			if (animator.GetBool ("Die"))
				return;
			if (animator) {
				float h = CrossPlatformInputManager.GetAxis ("Horizontal");
				float v = CrossPlatformInputManager.GetAxis ("Vertical");
				int k = 1;
				if (v < 0)
					k = -1;
				animator.SetFloat ("Speed", (h * h + v * v) * k * speed);
				animator.SetFloat ("Direction", h, DirectionDampTime, Time.deltaTime);	
			}

			//avoids the mouse looking if the game is effectively paused
			if (Mathf.Abs (Time.timeScale) < float.Epsilon)
				return;

#if !MOBILE_INPUT
			float yRot = Input.GetAxis ("Mouse X");
			float xRot = Input.GetAxis ("Mouse Y") * -1;


			transform.Rotate (new Vector3 (0, yRot, 0));
			camera.transform.Rotate (new Vector3 (xRot, 0, 0));
			if (Input.GetKeyDown (KeyCode.E)) {
				SwitchBullet ();
			}
			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				Blink ();
			}
		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			if (Cursor.lockState == CursorLockMode.Locked) {
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				mode = PlayMode.view;
			} else {
				Cursor.lockState = CursorLockMode.Locked;
				mode = PlayMode.play;
			}
		}
#endif
	}

	public void AddScore (int deltaScore)
	{
		score += deltaScore;
		if (score > maxScore) {
			PlayerPrefs.SetInt ("MaxScore", score);
			maxScore = score;
		}
		scoreText.text = "Score: " + score.ToString () + "\nMax Score: " + maxScore;
	}

	public void ChangeBullet (BulletType type)
	{
		bulletType = type;
		bulletText.text = "Bullet: " + type.ToString ();
//		;
	}


	public void SwitchBullet ()
	{
		if (bulletType == BulletType.Normal) {
			ChangeBullet (BulletType.Anaesthetic);
		} else if (bulletType == BulletType.Anaesthetic) {
			ChangeBullet (BulletType.Grenade);
		} else {
			ChangeBullet (BulletType.Normal);
		}
	}

	public void Reset ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void Blink ()
	{
		transform.Translate (transform.forward * 5);
	}
		
}

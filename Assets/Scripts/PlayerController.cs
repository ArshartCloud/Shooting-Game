using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
	public bool ApplyGravity = true;

	public Text scoreText;
	public PlayMode mode = PlayMode.play;

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
	}

	void Update ()
	{
		if (mode == PlayMode.play) {
			if (animator.GetBool ("Die"))
				return;
			if (animator) {
				float h = Input.GetAxis ("Horizontal");
				float v = Input.GetAxis ("Vertical");
				int k = 1;
				if (v < 0)
					k = -1;
				animator.SetFloat ("Speed", (h * h + v * v) * k);
				animator.SetFloat ("Direction", h, DirectionDampTime, Time.deltaTime);	
			}

			//avoids the mouse looking if the game is effectively paused
			if (Mathf.Abs (Time.timeScale) < float.Epsilon)
				return;

			float yRot = Input.GetAxis ("Mouse X") * speed;
			float xRot = Input.GetAxis ("Mouse Y") * -1 * speed;

			transform.Rotate (new Vector3 (0, yRot, 0));
			camera.transform.Rotate (new Vector3 (xRot, 0, 0));
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
}

using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{
	private AudioSource audioSource;

	// Use this for initialization
	void Start ()
	{
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.O)) {
			audioSource.volume = Mathf.Clamp (audioSource.volume + 0.1f, 0, 1.0f);
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			audioSource.volume = Mathf.Clamp (audioSource.volume - 0.1f, 0, 1.0f);
		}
	}

	void OnGUI ()
	{
		GUI.Label (new Rect (50, 50, 500, 50), "Current Music Volume: " + audioSource.volume.ToString () + "\nPress O and P to adjust");
	}

}

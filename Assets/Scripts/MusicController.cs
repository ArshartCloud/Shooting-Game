using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
	public Text musicText;
	private AudioSource audioSource;

	// Use this for initialization
	void Start ()
	{
		audioSource = GetComponent<AudioSource> ();
		musicText.text = "Current Music Volume: " + ((int)(audioSource.volume * 100)).ToString () + "\nPress O and P to adjust";
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.O)) {
			audioSource.volume = Mathf.Clamp (audioSource.volume + 0.1f, 0, 1.0f);
			musicText.text = "Current Music Volume: " + ((int)(audioSource.volume * 100)).ToString () + "\nPress O and P to adjust";
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			audioSource.volume = Mathf.Clamp (audioSource.volume - 0.1f, 0, 1.0f);
			musicText.text = "Current Music Volume: " + ((int)(audioSource.volume * 100)).ToString () + "\nPress O and P to adjust";
		}
	}

}

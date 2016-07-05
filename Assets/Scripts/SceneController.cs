using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.K)) {
			if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level1"))
				SceneManager.LoadScene ("Level2");
			if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level2"))
				SceneManager.LoadScene ("Level1");
		}
	}

	void OnGUI ()
	{
		GUI.Label (new Rect (300, 50, 200, 50), "Press K to Skip to next Level");
	}
}
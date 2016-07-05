using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour
{

	public void switchActive (GameObject obj)
	{
//		Debug.Log ("&&&");
		if (obj.activeInHierarchy) {
			obj.SetActive (false);
		} else {
			obj.SetActive (true);
		}

//		obj.SetActive (true);
	}
}

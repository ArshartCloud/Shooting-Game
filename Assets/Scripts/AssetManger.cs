using UnityEngine;
using System.Collections;

public class AssetManger : MonoBehaviour
{
	public GameObject bullet;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine (LoadBullet ("file://" + Application.dataPath + "/ABs/prefab/bullet"));
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	private IEnumerator LoadBullet (string path)
	{
		WWW bundlel = new WWW (path);
		yield return bundlel;
		bullet = bundlel.assetBundle.LoadAsset ("Bullet") as GameObject;
	}
}

using UnityEditor;
using UnityEngine;
using System.Collections;

public class Prepare : MonoBehaviour
{

	[MenuItem ("Custome Editor/Create AssetBunldes Main")]
	static void CreateAssetBunldesMain ()
	{
//		Object[] selectedAsset = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets);
//
//		foreach (Object obj in selectedAsset) {
//			string sourcePath = AssetDatabase.GetAssetPath (obj);
//			string targetPath = Application.dataPath + "/StreamingAssets/" + obj.name + ".assetbundle";
//			BuildPipeline.BuildAssetBundles (targetPath, BuildAssetBundleOptions.CollectDependencies);
//		}
		BuildPipeline.BuildAssetBundles ("Assets/ABs", BuildAssetBundleOptions.None);
//		BuildPipeline.buil
	}
}
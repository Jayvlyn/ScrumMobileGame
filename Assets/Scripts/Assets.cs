using UnityEngine;

/*
 * Global Asset references
 * Edit Asset references in the prefab Assets/AssetRefs
 * */
public class Assets : MonoBehaviour
{
	// Internal instance reference
	private static Assets _i;

	// Instance reference
	public static Assets i
	{
		get
		{
			if (_i == null) _i = Instantiate(Resources.Load<Assets>("AssetRefs"));
			return _i;
		}
	}

	[Header("Assets")]
	public Transform defaultPopup;
}

using GameEvents;
using UnityEngine;

/*
 * Global Asset references
 * Edit Asset references in the prefab Resources/AssetRefs
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
	public Transform damageEnemyPopup;
	public Transform takeDamagePopup;
	public FloatEvent OnPlayerHealthUpdated;
	public IntEvent OnPlayerScoreUpdated;
	public VoidEvent OnPlayerDeath;
	public StringEvent DoSceneChange;
}

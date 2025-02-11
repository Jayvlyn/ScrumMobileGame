using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
   public void ChangeScene(string sceneName)
	{
		SceneManager.LoadSceneAsync(sceneName);
	}


	public void QuitGame()
	{
		//supposedly this is the prefered way to handle android application closing

		AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
		activity.Call<bool>("moveTaskToBack", true);
	}
}

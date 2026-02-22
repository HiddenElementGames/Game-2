using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
		// Load the home scene first, then load all scenes so each menu can be activated without unnecessary loading
#if UNITY_EDITOR
		SceneManager.LoadScene((int)SceneID.Home);
#endif
		SceneManager.LoadSceneAsync((int)SceneID.Defense, LoadSceneMode.Additive);
		SceneManager.LoadSceneAsync((int)SceneID.OptionsCredits, LoadSceneMode.Additive);
		SceneManager.LoadSceneAsync((int)SceneID.Wild, LoadSceneMode.Additive);
	}
}

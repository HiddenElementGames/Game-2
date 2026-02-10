using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
		// Load the home scene first, then load all scenes so each menu can be activated without unnecessary loading
        SceneManager.LoadScene((int)SceneID.Home);
        SceneManager.LoadSceneAsync((int)SceneID.Craft, LoadSceneMode.Additive);
		SceneManager.LoadSceneAsync((int)SceneID.Defense, LoadSceneMode.Additive);
		SceneManager.LoadSceneAsync((int)SceneID.OptionsCredits, LoadSceneMode.Additive);
		SceneManager.LoadSceneAsync((int)SceneID.Sacrifice, LoadSceneMode.Additive);
		SceneManager.LoadSceneAsync((int)SceneID.Survivor, LoadSceneMode.Additive);
		SceneManager.LoadSceneAsync((int)SceneID.Wild, LoadSceneMode.Additive);
	}
}

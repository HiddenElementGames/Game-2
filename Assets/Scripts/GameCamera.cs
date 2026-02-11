using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] public SceneID sceneID;

    void Awake()
    {
        AddCameraToMenuNavigation();

		// Deactivate all cameras except the home scene camera
		if (sceneID != SceneID.Home)
        {
            gameObject.SetActive(false);
        }
	}

	/// <summary>
	/// Adds this camera to the MenuNavigation system so it can be activated when the corresponding menu is selected
	/// </summary>
	private void AddCameraToMenuNavigation()
    {
        MenuNavigation menu = FindAnyObjectByType<MenuNavigation>();
        if (menu != null)
        {
            menu.SceneCameras.Add(sceneID, gameObject);
        }
	}
}

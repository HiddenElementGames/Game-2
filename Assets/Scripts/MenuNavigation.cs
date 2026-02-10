using System.Collections.Generic;
using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    public Dictionary<SceneID, GameObject> SceneCameras = new Dictionary<SceneID, GameObject>();

    public void SwitchToScene(int sceneID)
    {
        // Deactivate all cameras
        foreach (GameObject camera in SceneCameras.Values)
        {
            camera.SetActive(false);
        }

        // Activate the camera for the selected menu
        SceneCameras[(SceneID)sceneID].SetActive(true);
    }
}

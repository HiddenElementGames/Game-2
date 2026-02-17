using UnityEngine;
using UnityEngine.InputSystem;

public class AttackZombiesInteraction : MonoBehaviour
{
    private Camera cam;
    private Mouse mouse;

    private void Start()
    {
        cam = GetComponent<Camera>();
        mouse = Mouse.current;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // only run if the player pressed the LMB
        if(mouse.leftButton.wasPressedThisFrame)
        {
            // create a ray from the camera at the current mouse position
            Ray ray = cam.ScreenPointToRay(mouse.position.value);
            RaycastHit hit;

            // if the player hit a zombie, kill it
            if(Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Zombie"))
            {
                hit.transform.gameObject.GetComponent<ZombieDeath>().Die();
			}
        }
    }
}

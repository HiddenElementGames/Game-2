using UnityEngine;
using UnityEngine.InputSystem;

public class AttackZombiesInteraction : MonoBehaviour
{
    private Camera cam;
    private Mouse mouse;

    private AudioSource buttonAudio;

    private void Start()
    {
        cam = GetComponent<Camera>();
        mouse = Mouse.current;

        AudioSource[] audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach(AudioSource audio in audioSources)
        {
            if(audio.gameObject.tag == "Button")
            {
                buttonAudio = audio;
            }
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // only run if the player pressed the LMB
        if(mouse.leftButton.wasPressedThisFrame && GameManager.Instance.AmmoCount > 0)
        {
            // create a ray from the camera at the current mouse position
            Ray ray = cam.ScreenPointToRay(mouse.position.value);
            RaycastHit hit;

            // if the player hit a zombie, kill it
            if(Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Zombie"))
            {
                GameManager.Instance.AmmoCount--;
                hit.transform.gameObject.GetComponent<ZombieDeath>().Die();
                buttonAudio.PlayOneShot(buttonAudio.clip);
			}
        }
    }
}

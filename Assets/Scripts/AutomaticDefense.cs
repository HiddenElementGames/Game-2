using UnityEngine;

public class AutomaticDefense : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Zombie"))
		{
			// only try to defend the base if there are available defenses, or if there are survivors and ammo
			if (GameManager.Instance.SurvivorCount > 0 && GameManager.Instance.AmmoCount > 0)
			{
				Destroy(other.gameObject);
			}
			else if(GameManager.Instance.DefenseItemCount > 0)
			{
				Destroy(other.gameObject);
			}
			// keeping the above 2 situations separate, that way we can properly subtract defense count or ammo count when the time comes
		}
	}
}

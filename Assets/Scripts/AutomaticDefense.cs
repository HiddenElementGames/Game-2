using System.Collections;
using UnityEngine;

public class AutomaticDefense : MonoBehaviour
{
	[SerializeField, Min(0)] private int minAmmoCost;
	[SerializeField, Min(0)] private int maxAmmoCost;
	[SerializeField, Min(0)] private float zombieKillDelay;

	private WaitForSeconds ZombieKillDelay;

	private void Start()
	{
		ZombieKillDelay = new WaitForSeconds(zombieKillDelay);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Zombie"))
		{
			// only try to defend the base if there are available defenses, or if there are survivors and ammo
			if (GameManager.Instance.SurvivorCount > 0 && GameManager.Instance.AmmoCount > 0)
			{
				StartCoroutine(AttackZombieDelayed(other.gameObject));
			}
			else if(GameManager.Instance.DefenseItemCount > 0)
			{
				GameManager.Instance.DefenseItemCount--;
				other.gameObject.GetComponent<ZombieDeath>().Die();
			}
			else
            {
                StartCoroutine(ZombieAttackSurvivors());
            }
			// keeping the above 2 situations separate, that way we can properly subtract defense count or ammo count when the time comes
		}
	}

	private IEnumerator ZombieAttackSurvivors()
    {
		yield return zombieKillDelay;
        while(true)
        {
            yield return new WaitForSeconds(1f);
			if(GameManager.Instance.SurvivorCount > 0)
            {
                int killCount = Mathf.Max(1, GameManager.Instance.SurvivorCount / 100);
				GameManager.Instance.SurvivorCount -= killCount;
            }
        }
    }

	private IEnumerator AttackZombieDelayed(GameObject zombie)
	{
		// wait
		yield return ZombieKillDelay;

		// consume ammo
		int ammoCost = Random.Range(minAmmoCost, maxAmmoCost + 1);
		ammoCost = Mathf.Min(ammoCost, GameManager.Instance.AmmoCount);
		GameManager.Instance.AmmoCount -= ammoCost;

		// kill zombie
		zombie.GetComponent<ZombieDeath>().Die();
	}
}

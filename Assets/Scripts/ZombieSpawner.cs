using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject zombiePrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float minSpawnTimer;
    [SerializeField] private float maxSpawnTimer;
    [SerializeField] private int minZombieCountScaling;
    [SerializeField] private int maxZombieCountScaling;

    [Header("Viewable fields (Do not edit)")]
    [SerializeField] private float zombieSpawnTime;

    private List<GameObject> activeZombies; // keeps track of the amount of zombies spawned, to ensure the number doesn't surpass the actual amount of zombies

    /// <summary>
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    /// </summary>
    void Start()
    {
        activeZombies = new();
        StartCoroutine(SpawnZombies());
    }

    /// <summary>
    /// Calculates zombie spawn rate based on zombie count, and spawns zombies
    /// </summary>
    private IEnumerator SpawnZombies()
    {
        while(true)
        {
            // only spawn zombies if there are zombies, and if the currently spawned zombie count is lower than the manager count
            if(GameManager.Instance.ZombieCount > 0 && activeZombies.Count < GameManager.Instance.ZombieCount)
            {
                GameObject newZombie = Instantiate(zombiePrefab, transform.position, Quaternion.identity);
                newZombie.transform.position += new Vector3(0, 0, Random.Range(-5, 5));

                activeZombies.Add(newZombie);
            }

            // calculate time to next spawn
            if(GameManager.Instance.ZombieCount > minZombieCountScaling)
            {
                // Get the zombie count, cap the amount at the max, and remove the min number so scaling is proper
				float zombieCount = Mathf.Min(GameManager.Instance.ZombieCount, maxZombieCountScaling);
                zombieCount -= minZombieCountScaling;

				float zombieDifference = maxZombieCountScaling - minZombieCountScaling;

				float zombieScale = zombieCount / zombieDifference;

				float curvePoint = -(Mathf.Cos(Mathf.PI * zombieScale) - 1) / 2;

				float timerDifference = maxSpawnTimer - minSpawnTimer;

				float timeReduction = curvePoint * timerDifference;

                zombieSpawnTime = maxSpawnTimer - timeReduction;
			}
            else
            {
                zombieSpawnTime = maxSpawnTimer;
            }

            // make sure time is above the minimum
            zombieSpawnTime = Mathf.Max(zombieSpawnTime, minSpawnTimer);

            // wait until the next zombie spawn
            yield return new WaitForSeconds(zombieSpawnTime);
        }
    }
}

using UnityEngine;

public class ScavengeAction : MonoBehaviour
{
    [Header("Time")]
    [SerializeField, Tooltip("The default time it takes to complete this task"), Min(0)] private float scavengeTimeSeconds;
	[SerializeField, Tooltip("The minimum time this task can take regardless of survivor count"), Min(0)] private float minScavengeTimeSeconds; // logic not implemented
	[SerializeField, Tooltip("The rate that survivors affect the time scale for this task. Rate currently unknown, range unknown.")] private float survivorTimeScaleRate; // logic not implemented

    [Header("Resources")]
    [SerializeField, Tooltip("The minimum amount of food the player can gather from this task"), Min(0)] private int minFoodGathered;
    [SerializeField, Tooltip("The maximum amount of food the player can gather from this task"), Min(0)] private int maxFoodGathered;
    [SerializeField, Tooltip("The minimum amount of ammo the player can gather from this task"), Min(0)] private int minAmmoGathered;
    [SerializeField, Tooltip("The maximum amount of ammo the player can gather from this task"), Min(0)] private int maxAmmoGathered;
    [SerializeField, Tooltip("The minimum amount of survivors the player can find from this task"), Min(0)] private int minSurvivorsFound;
	[SerializeField, Tooltip("The maximum amount of survivors the player can find from this task"), Min(0)] private int maxSurvivorsFound;

	[Header("Risks")]
    [SerializeField, Tooltip(""), Range(0,1)] private float zombieIncreaseChance; // logic not implemented
    [SerializeField, Tooltip(""), Min(0)] private int minZombieIncreaseCount; // logic not implemented
    [SerializeField, Tooltip(""), Min(0)] private int maxZombieIncreaseCount; // logic not implemented
    [SerializeField, Tooltip(""), Range(0,1)] private float zombieAttackChance; // logic not implemented

    private float currentScavengeTimeSeconds;
    private bool active;

    /// <summary>
    /// Activates the task, used on buttons
    /// </summary>
    public void ActivateTask()
    {
        active = true;
    }

    /// <summary>
    /// Deactivates the task, used on buttons
    /// </summary>
    public void DeactivateTask()
    {
        active = false;
        currentScavengeTimeSeconds = 0;
    }

	/// <summary>
	/// Called when the currentScavengeTimeSeconds passes the scavengeTimeSeconds, and provides the player resources for
	/// finishing this task.
	/// </summary>
	private void CompleteTask()
    {
        GameManager.Instance.FoodCount += Random.Range(minFoodGathered, maxFoodGathered + 1);
        GameManager.Instance.AmmoCount += Random.Range(minAmmoGathered, maxAmmoGathered + 1);
        GameManager.Instance.SurvivorCount += Random.Range(minSurvivorsFound, maxSurvivorsFound + 1);
    }

    void Update()
    {
        if(active)
        {
            // update task time. If task is complete, give player resources
            currentScavengeTimeSeconds += Time.deltaTime;
            if(currentScavengeTimeSeconds >= scavengeTimeSeconds)
            {
                currentScavengeTimeSeconds -= scavengeTimeSeconds;
                CompleteTask();
            }
        }
    }
}

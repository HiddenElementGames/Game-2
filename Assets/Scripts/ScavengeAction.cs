using UnityEngine;
using UnityEngine.UI;

public class ScavengeAction : MonoBehaviour
{
    [Header("Time")]
    [SerializeField, Tooltip("The default time it takes to complete this task"), Min(0)] private float defaultScavengeTimeSeconds;
	[SerializeField, Tooltip("The minimum time this task can take regardless of survivor count"), Min(0)] private float minScavengeTimeSeconds;
	[SerializeField, Tooltip("The rate that survivors affect the time scale for this task."), Min(0)] private float survivorTimeScaleRate;

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

    [Header("UI")]
    [SerializeField] private Slider taskProgressBar;

    private float currentScavengeTimeSeconds; // time spent doing the current task
    private float scavengeTimeSecondsScaled; // The time it takes to complete this task, scaled based on survivor count
    private bool active; // whether this task is active or not

    /// <summary>
    /// Activates the task, used on buttons
    /// </summary>
    public void ActivateTask()
    {
        // update the sliders task time in case the time required has changed
        UpdateSliderMaxValue();

        // deactivate other active tasks
		ScavengeAction[] actions = FindObjectsByType<ScavengeAction>(FindObjectsSortMode.None);
		foreach (ScavengeAction action in actions)
		{
            action.DeactivateTask();
		}

        // activate this task
		active = true;
    }

    /// <summary>
    /// Deactivates the task
    /// </summary>
    public void DeactivateTask()
    {
        active = false;
        currentScavengeTimeSeconds = 0;
        taskProgressBar.value = 0;
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

        // Update the task time in case it has changed. Example, if the player has more survivors now, the task should be faster
        UpdateSliderMaxValue();
	}

    /// <summary>
    /// Updates the max value for the slider based on the complete task time
    /// </summary>
    private void UpdateSliderMaxValue()
    {
        ScaleScavengeTime();
		taskProgressBar.maxValue = scavengeTimeSecondsScaled;
    }

    /// <summary>
    /// Scales the scavenge time based on current number of survivors at the scale rate
    /// TaskTime = defaultTime / (1 + scaleRate)^SurvivorCount
    /// </summary>
    private void ScaleScavengeTime()
    {
        scavengeTimeSecondsScaled = Mathf.Clamp(
                                        defaultScavengeTimeSeconds / Mathf.Pow(1 + survivorTimeScaleRate, GameManager.Instance.SurvivorCount), // value for clamp
                                        minScavengeTimeSeconds, // min for clamp
                                        Mathf.Infinity); // max for clamp
    }

    private void Start()
    {
        // Update the task slider max value to represent the time it takes to complete this task
        UpdateSliderMaxValue();
	}

    private void Update()
    {
        if(active)
        {
            // update task time. If task is complete, give player resources
            currentScavengeTimeSeconds += Time.deltaTime;
            if(currentScavengeTimeSeconds >= scavengeTimeSecondsScaled)
            {
                currentScavengeTimeSeconds -= scavengeTimeSecondsScaled;
                CompleteTask();
            }

            // Update the task slider graphics
            taskProgressBar.value = currentScavengeTimeSeconds;
        }
    }
}

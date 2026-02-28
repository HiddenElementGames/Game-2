using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameManager singleton, can be referenced with "GameManager.Instance" from any script
    public static GameManager Instance { get; private set; }

    // Inventory counts for the player. Logic should be in it's own script, the manager just holds the data so other
    // scripts don't have to know about each other.
    // Can be referenced with "GameManager.Instance.SurvivorCount" for example from any script
    public int SurvivorCount = 10;
    public int FoodCount;
    public int AmmoCount = 20;
    public int ResourceCount;
    public int HealingItemCount;
    public int DefenseItemCount = 5;
    public int ZombieCount = 120;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        // Creates a game manager instance when the game is loaded and ensure it does not get destroyed when changing scenes
        GameObject gameManager = new GameObject("Game Manager");
        gameManager.AddComponent<GameManager>();
        DontDestroyOnLoad(gameManager);
        Instance = gameManager.GetComponent<GameManager>();
	}
}
using TMPro;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI survivorCountText;
    [SerializeField] private TextMeshProUGUI zombieCountText;
    [SerializeField] private TextMeshProUGUI ammoCountText;
    [SerializeField] private TextMeshProUGUI defenseCountText;

    private void Update()
    {
        survivorCountText.text = "Survivors: " + GameManager.Instance.SurvivorCount;
        zombieCountText.text = "Zombies: " + GameManager.Instance.ZombieCount;
        ammoCountText.text = "Ammo: " + GameManager.Instance.AmmoCount;
        defenseCountText.text = "Defenses: " + GameManager.Instance.DefenseItemCount;
    }
}

using UnityEngine;

public class ZombieDeath : MonoBehaviour
{
    [SerializeField] private LootDrop[] lootDrops; 

    public void Die()
    {
        //RollLoot();
        Destroy(gameObject);
    }

    private void RollLoot()
    {
        foreach(LootDrop loot in lootDrops)
        {
            if(Random.Range(0f,1f) < loot.DropChance)
            {
                Instantiate(loot.LootItem, transform.position, Quaternion.identity);
            }
        }
    }
}

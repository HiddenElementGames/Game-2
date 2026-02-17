using UnityEngine;

[System.Serializable]
public class LootDrop
{
    [SerializeField, Range(0f,1f)] public float DropChance;
    [SerializeField] public GameObject LootItem;
}

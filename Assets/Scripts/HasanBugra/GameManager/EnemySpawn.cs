using System;
using UnityEngine;

[Serializable]
public class EnemySpawn
{
    public GameObject enemyPrefab;
    [Range(0f, 100f)] public float spawnChance = 100f; 
}
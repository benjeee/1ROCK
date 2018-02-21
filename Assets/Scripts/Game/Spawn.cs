using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spawn{

    public enum SpawnType
    {
        Ground,
        Flying,
        OutOfBounds,
        AirBoss
    }

    [SerializeField]
    Transform prefab;
    public Transform Prefab
    {
        get { return prefab; }
    }

    [SerializeField]
    bool effectsEnemyCount;
    public bool EffectsEnemyCount
    {
        get { return effectsEnemyCount; }
    }

    [SerializeField]
    int numToSpawn;
    public int NumToSpawn
    {
        get { return numToSpawn; }
    }

    [SerializeField]
    SpawnType enemySpawnType;
    public SpawnType EnemySpawnType
    {
        get { return enemySpawnType; }
    }
}

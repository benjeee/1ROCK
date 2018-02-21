using UnityEngine;

[System.Serializable]
public class Wave {

    [SerializeField]
    Spawn[] EnemySpawns;

    int numEnemies;

    WaveManager waveManager;

    public void Begin()
    {
        numEnemies = 0;
        waveManager = GameManager.instance.waveManager;
        foreach(Spawn spawn in EnemySpawns)
        {
            for(int i = 0; i < spawn.NumToSpawn; i++)
            {
                Transform enemy;
                if (spawn.EnemySpawnType == Spawn.SpawnType.Ground)
                {
                    enemy = GameManager.Instantiate(spawn.Prefab, waveManager.PickNextGroundSpawn(), Quaternion.identity);    
                } else if (spawn.EnemySpawnType == Spawn.SpawnType.Flying)
                {
                    enemy = GameManager.Instantiate(spawn.Prefab, waveManager.PickNextAirSpawn(), Quaternion.identity);
                } else if(spawn.EnemySpawnType == Spawn.SpawnType.AirBoss)
                {
                    enemy = GameManager.Instantiate(spawn.Prefab, waveManager.PickNextAirBossSpawn(), Quaternion.identity);
                } else //Out of bounds spawn
                {
                    enemy = GameManager.Instantiate(spawn.Prefab, waveManager.PickNextOOBSpawn(), Quaternion.identity);//may have to get a better rotation
                }
                enemy.GetComponent<Enemy>().wave = this;
            }
            if (spawn.EffectsEnemyCount)
            {
                numEnemies += spawn.NumToSpawn;
            }
        }
    }
    /*
    [SerializeField]
    int numWalkers;

    [SerializeField]
    int numFlyerThiefs;

    int numEnemies;

    WaveManager waveManager;

    public void Begin()
    {
        numEnemies = 0;
        waveManager = GameManager.instance.waveManager;
        int i;
        for(i = 0; i < numWalkers; i++)
        {
            Transform enemy = GameManager.Instantiate(ResourceManager.instance.BasicWalkerPrefab, waveManager.PickNextGroundSpawn(), Quaternion.identity);
            enemy.GetComponent<Enemy>().wave = this;
            numEnemies++;
        }
        for (i = 0; i < numFlyerThiefs; i++)
        {
            Transform enemy = GameManager.Instantiate(ResourceManager.instance.FlyerThiefPrefab, waveManager.PickNextAirSpawn(), Quaternion.identity);
            enemy.GetComponent<Enemy>().wave = this;
            numEnemies++;
        }
    }
    */

    public void RegisterEnemyDead(Enemy enemy)
    {
        numEnemies--;
        if(numEnemies == 0)
        {
            NotifyWaveFinished();
        }
    }

    void NotifyWaveFinished()
    {
        GameManager.instance.waveManager.RegisterWaveFinished(this);
    }
}

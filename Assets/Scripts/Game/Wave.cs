using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave {

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
            Transform enemy = GameManager.Instantiate(waveManager.BasicWalkerPrefab, waveManager.PickNextGroundSpawn(), Quaternion.identity);
            enemy.GetComponent<Enemy>().wave = this;
            numEnemies++;
        }
        for (i = 0; i < numFlyerThiefs; i++)
        {
            Transform enemy = GameManager.Instantiate(waveManager.FlyerThiefPrefab, waveManager.PickNextAirSpawn(), Quaternion.identity);
            enemy.GetComponent<Enemy>().wave = this;
            numEnemies++;
        }
    }

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

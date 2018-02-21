using UnityEngine;

[CreateAssetMenu(fileName = "WaveSet", menuName = "WaveManager", order = 1)]
public class WaveManager : ScriptableObject{

    [SerializeField]
    Wave[] waves;

    int currWave = 0;
    public Vector3[] groundSpawns;
    public Vector3[] airSpawns;
    public Vector3[] airBossSpawns;
    public Vector3[] oobSpawns;
    int groundSpawnIndex;
    int airSpawnIndex;
    int oobSpawnIndex;
    int airBossSpawnIndex;

    public void Init()
    {
        currWave = 0;
        resetIndices();
    }

    void resetIndices()
    {
        groundSpawnIndex = 0;
        airSpawnIndex = 0;
        oobSpawnIndex = 0;
        airBossSpawnIndex = 0;
    }

    public void StartNextWave()
    {
        if(currWave < waves.Length)
        {
            resetIndices();
            waves[currWave].Begin();
            currWave++;
        }
    }

    public void RegisterWaveFinished(Wave wave)
    {
        StartNextWave();
    }

    public Vector3 PickNextGroundSpawn()
    {
        if(groundSpawnIndex < groundSpawns.Length)
        {
            return groundSpawns[groundSpawnIndex++];
        }
        else
        {
            Debug.LogError("Spawning more ground enemies than available spawns!");
            return new Vector3(0,0,0);
        }
    }

    public Vector3 PickNextAirSpawn()
    {
        if (airSpawnIndex < airSpawns.Length)
        {
            return airSpawns[airSpawnIndex++]; 
        }
        else
        {
            Debug.LogError("Spawning more air enemies than available spawns!");
            return new Vector3(0, 30, 0);
        }
    }

    public Vector3 PickNextOOBSpawn()
    {
        if(oobSpawnIndex < oobSpawns.Length)
        {
            return oobSpawns[oobSpawnIndex++];
        }
        else
        {
            Debug.LogError("Spawning more OOB enemies than available spawns!");
            return new Vector3(0, 30, 0);
        }
    }

    public Vector3 PickNextAirBossSpawn()
    {
        if (airBossSpawnIndex < airBossSpawns.Length)
        {
            return airBossSpawns[airBossSpawnIndex++];
        }
        else
        {
            Debug.LogError("Spawning more Airboss enemies than available spawns!");
            return new Vector3(0, 30, 0);
        }
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "WaveSet", menuName = "WaveManager", order = 1)]
public class WaveManager : ScriptableObject{

    [SerializeField]
    Wave[] waves;

    int currWave = 0;
    public Vector3[] groundSpawns;
    public Vector3[] airSpawns;
    int groundSpawnIndex;
    int airSpawnIndex;

    public void Init()
    {
        currWave = 0;
        groundSpawnIndex = 0;
        airSpawnIndex = 0;
    }

    public void StartNextWave()
    {
        if(currWave < waves.Length)
        {
            groundSpawnIndex = 0;
            airSpawnIndex = 0;
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
            Debug.LogError("Spawning more enemies than available spawns!");
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
            Debug.LogError("Spawning more enemies than available spawns!");
            return new Vector3(0, 30, 0);
        }
    }
}

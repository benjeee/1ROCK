using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public Player player;
    public WaveManager waveManager;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than 1 GameManager instantiated");
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        waveManager.Init();
        waveManager.StartNextWave();
    }

    public void SlowForSeconds(float time)
    {
        StartCoroutine(TimeScaleShiftCoroutine(time));
    }

    IEnumerator TimeScaleShiftCoroutine(float time)
    {
        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(time);
        Time.timeScale = 1;
    }
}

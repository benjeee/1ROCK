using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public Player player;

    public const float xBound = 50f;
    public const float zBound = 50f;
    public const float yBound = 100f;

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

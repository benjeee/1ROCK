using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    public static ResourceManager instance;

    public Transform BasicWalkerPrefab;
    public Transform FlyerThiefPrefab;
    public Transform RockSnakePrefab;

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

}

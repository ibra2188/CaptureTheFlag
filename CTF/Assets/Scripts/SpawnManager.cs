using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{

    public static SpawnManager Instance;

    public SpawnPoint[] spawnPoints;

    private void Awake()
    {
        Instance = this;
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
    }
    
    public Transform GetSpawnPointTeam1()
    {
        Transform final = spawnPoints[0].transform;
        Vector3 temp = final.position;
        temp.z += Random.Range(-2, 6);
        temp.x += Random.Range(0, 5);
        final.position = temp;
        return final ;
    }

    public Transform GetSpawnPointTeam2()
    {
        Transform final = spawnPoints[1].transform;
        Vector3 temp = final.position;
        temp.z += Random.Range(-2, 6);
        temp.x += Random.Range(-5, 0);
        final.position = temp;
        return final ;
    }
}
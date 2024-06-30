using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class ZombieSpawnController : MonoBehaviour
{
    public GameObject zombiePrefab;

    public List<Transform> spawnPoints = new List<Transform>();
    //public Transform defaultSpawn;
    
    private int spawnsActive;

    // Start is called before the first frame update
    void Start()
    {
        //defaultSpawn.transform.position = new Vector3(10,10,0);
        spawnsActive = Random.Range(10, 15);

        for (int i = 0; i < spawnsActive; i++)
        {
            Instantiate(zombiePrefab, spawnPoints[Random.Range(0,spawnPoints.Count)]);
        }
    }

    void FixedUpdate()
    {

    }
}

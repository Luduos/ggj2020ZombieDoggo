﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using de.crystalmesh;

public class LaneSpawner : MonoBehaviour
{
    [SerializeField]
    private Lane[] lanePrefabs = null;

    private List<Lane> spawnedLanes = new List<Lane>();

    private void Awake()
    {
        spawnedLanes.Add(FindObjectOfType<Lane>());
        foreach(Lane lane in spawnedLanes)
        {
            lane.OnSpawnNextLane += OnSpawnNewLane;
        }
    }

    public void OnSpawnNewLane(Lane origin)
    {
        Lane prefabTypeToSpawn = Utilities.RandomFromArray(lanePrefabs);

        Lane spawnedLane = Instantiate(prefabTypeToSpawn.gameObject).GetComponent<Lane>();
        spawnedLane.transform.position = origin.NextLaneSpawnPoint.position;
        spawnedLane.OnSpawnNextLane += OnSpawnNewLane;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SniperDemo
{
    public class SpawnRunners : MonoBehaviour
    {
        public List<Transform> spawnPointList;
        public GameObject enemy;
        public int maxEnemies;
        
        void Start()
        {
            InvokeRepeating("SpawnEnemy", 1.4f, 1.4f);
        }

        void SpawnEnemy()
        {
            GameObject [] enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy");

            if (enemiesInScene.Length < maxEnemies)
            {
                int random = Random.Range(0, spawnPointList.Count);
                Instantiate(enemy, spawnPointList[random].position, spawnPointList[random].rotation);
            }
        }
    }
}
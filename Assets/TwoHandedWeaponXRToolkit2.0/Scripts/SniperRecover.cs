using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRecover : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sniper"))
        {
            other.transform.position = spawnPosition.position;
            other.transform.rotation = spawnPosition.rotation;
        }
    }
}
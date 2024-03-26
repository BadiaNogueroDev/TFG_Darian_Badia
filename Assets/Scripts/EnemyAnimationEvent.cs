using System.Collections;
using System.Collections.Generic;
using SniperDemo;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    private EnemyController enemy;

    void Start()
    {
        enemy = GetComponentInParent<EnemyController>();
    }

    public void Shoot()
    {
        enemy.FireBullet();
    }
}

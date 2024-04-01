using System;
using UnityEngine;
namespace SniperDemo
{
    public class Projectile : MonoBehaviour
    {
        private int playerForce = 50;
        private int enemyForce = 15;
        private float lifetime = 3;
        
        public void Launch()
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.AddRelativeForce(Vector3.forward * playerForce, ForceMode.Impulse);
            Destroy(gameObject, lifetime);
        }
        
        public void LaunchAtPlayer(Vector3 direction)
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.AddRelativeForce(direction * enemyForce, ForceMode.Impulse);
            Destroy(gameObject, lifetime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (CompareTag("Bullet") && other.CompareTag("Enemy"))
            {
                EnemyController enemy = other.GetComponent<EnemyController>();
                enemy.Die();
                Destroy(gameObject);
            }
            if (CompareTag("BulletFromEnemy") && other.CompareTag("Player"))
            {
                HealthSystem player = other.GetComponent<HealthSystem>();
                player.GetDamage(10);
                Destroy(gameObject);
            }
        }
    }
}
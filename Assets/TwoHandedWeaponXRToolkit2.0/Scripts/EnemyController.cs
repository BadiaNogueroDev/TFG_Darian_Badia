using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace SniperDemo
{
    public class EnemyController : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;
        private Animator animator;
        private GameObject player;
        
        [SerializeField] private GameObject bullet;
        [SerializeField] private Transform shotPoint;
        
        private bool dead;
        private float playerDistance;
        private float playerDistanceToShoot = 5.0f;
        private float shotTimer;
        private float shotTimerMax = 3.0f;
        
        void Start()
        {
            dead = false;
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
            player = GameObject.Find("XR Rig");
        }

        void Update()
        {
            playerDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
            
            if (!dead)
            {
                navMeshAgent.SetDestination(player.transform.position);
            }
            
            if(playerDistance <= playerDistanceToShoot)
            {
                navMeshAgent.isStopped = true;
                animator.SetTrigger("Stop");
                shotTimer += Time.deltaTime;
                transform.LookAt(player.transform);
                if (shotTimer >= shotTimerMax)
                {
                    FireBullet();
                    shotTimer = 0.0f;
                }
            }
        }

        private void FireBullet()
        {
            Projectile projectile = Instantiate(bullet, shotPoint.position, shotPoint.rotation).GetComponent<Projectile>();
            Vector3 playerDirection = transform.position - player.transform.position;
            playerDirection.Normalize();
            projectile.LaunchAtPlayer(playerDirection);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                dead = true;
                navMeshAgent.isStopped = true;
                animator.SetTrigger("Death");
                Destroy(gameObject, 3.0f);
            }
        }
    }
}
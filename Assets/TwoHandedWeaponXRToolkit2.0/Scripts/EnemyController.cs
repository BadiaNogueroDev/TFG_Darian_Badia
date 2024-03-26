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
        private bool playerInRange;
        private float playerDistance;
        private float playerDistanceToShoot = 5.0f;
        
        void Start()
        {
            dead = false;
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {
            playerDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
            
            if (!dead)
            {
                navMeshAgent.SetDestination(player.transform.position);
            }
            
            if(playerDistance <= playerDistanceToShoot && !playerInRange)
            {
                playerInRange = true;
                navMeshAgent.isStopped = true;
                transform.LookAt(player.transform);
                animator.SetTrigger("Stop");
            }
        }

        public void FireBullet()
        {
            Projectile projectile = Instantiate(bullet, shotPoint.position, shotPoint.rotation).GetComponent<Projectile>();
            projectile.LaunchAtPlayer(Vector3.forward);
        }

        public void Die()
        {
            Manager.instance.data.EnemiesKilled++;
            
            dead = true;
            navMeshAgent.isStopped = true;
            animator.SetTrigger("Death");
            Destroy(gameObject, 3.0f);
        }
    }
}
using UnityEngine;
namespace SniperDemo
{
    public class Projectile : MonoBehaviour
    {
        private int playerForce = 200;
        private int enemyForce = 10;
        private float lifetime = 2;
        
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
    }
}
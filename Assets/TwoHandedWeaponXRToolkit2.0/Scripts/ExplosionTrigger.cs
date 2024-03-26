using UnityEngine;

namespace SniperDemo
{
    public class ExplosionTrigger : MonoBehaviour
    {
        public GameObject explodingPieces;
        public AudioClip explosionClip;
        private Rigidbody rb;

        void Start()
        {
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            rb = gameObject.GetComponent<Rigidbody>();
            rb.AddForce(20 * gameObject.transform.forward, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Body" || collision.gameObject.tag == "Environment")
            {
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
                gameObject.GetComponent<AudioSource>().PlayOneShot(explosionClip);
                explodingPieces.SetActive(true);
                explodingPieces.transform.parent = null;
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Body")
            {
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
                gameObject.GetComponent<AudioSource>().PlayOneShot(explosionClip);
                explodingPieces.SetActive(true);
                explodingPieces.transform.parent = null;
                Destroy(gameObject);
            }
        }
    }
}
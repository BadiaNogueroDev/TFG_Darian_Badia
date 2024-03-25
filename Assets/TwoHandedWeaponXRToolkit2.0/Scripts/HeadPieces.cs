using System.Collections;
using UnityEngine;

namespace SniperDemo
{
    public class HeadPieces : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(LongWaiter());
        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Environment" || other.gameObject.tag == "Body")
                StartCoroutine(Waiter());
        }
        public IEnumerator Waiter()
        {
            yield return new WaitForSeconds(1.0f);
            Destroy(gameObject);
        }

        public IEnumerator LongWaiter()
        {
            yield return new WaitForSeconds(10.0f);
            Destroy(gameObject);
        }
    }
}

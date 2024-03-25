using System.Collections;
using UnityEngine;

namespace SniperDemo
{
    public class ActivateGrabPoint : MonoBehaviour
    {
        public GameObject grabPoint;
        public GameObject grabPoint2;

        void Start()
        {
            StartCoroutine(ActivateGrabPointDelayed());
        }

        public IEnumerator ActivateGrabPointDelayed()
        {
            yield return new WaitForSeconds(1.0f);
            grabPoint.SetActive(true);
            grabPoint2.SetActive(true);
        }
    }
}
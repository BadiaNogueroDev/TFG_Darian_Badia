using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace SniperDemo
{
    public class SocketChecking : MonoBehaviour
    {
        private XRSocketInteractor xRSocketInteractor;
        public GameObject rpgWarhead;
        public Transform launchPoint;
        public AudioClip rpgLoadAudioClip;
        public AudioSource rpgLoadAudioSource;
        public AudioClip rpgFireAudioClip;
        public AudioSource rpgFireAudioSource;
        private IXRSelectInteractable previousInteractable;
        public GameObject dummyMagazine;
        public XRSocketInteractor socket;
        public GameObject xrSocket;

        void Start()
        {
            xRSocketInteractor = GetComponent<XRSocketInteractor>();
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        }

        public void CheckDummyMag()
        {
            if (dummyMagazine.active)
            {
                dummyMagazine.SetActive(false);
                var rb = previousInteractable.transform.GetComponent<Rigidbody>();
                rb.useGravity = true;
                previousInteractable.transform.position = dummyMagazine.transform.position;
                previousInteractable.transform.rotation = dummyMagazine.transform.rotation;
                previousInteractable.transform.gameObject.SetActive(true);
                rb.constraints = RigidbodyConstraints.None;
            }
        }

        public void CheckSocket()
        {
            if (socket.hasSelection)
            {
                IXRSelectInteractable objName = socket.GetOldestInteractableSelected();
                previousInteractable = objName;
                objName.transform.gameObject.SetActive(false);
                dummyMagazine.SetActive(true);
                var rigB = objName.transform.gameObject.GetComponent<Rigidbody>();
                rigB.freezeRotation = true;
                rigB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
            }
            else
            {
                xrSocket.SetActive(false);
            }
        }

        public void Fire()
        {
            GameObject spawnedBullet = Instantiate(rpgWarhead, launchPoint.position, launchPoint.rotation);
            var rb = spawnedBullet.GetComponent<Rigidbody>();
            var col = spawnedBullet.GetComponent<BoxCollider>();
            rb.freezeRotation = true;
            rb.useGravity = false;
            rpgFireAudioSource.PlayOneShot(rpgFireAudioClip);
            Destroy(spawnedBullet, 3);
        }

        private IEnumerator Waiter(Rigidbody rb)
        {
            yield return new WaitForSeconds(1.0f);
        }

        private IEnumerator RotateWithVelocity(Rigidbody rigidbody)
        {
            yield return new WaitForFixedUpdate();
            Quaternion newRotation = Quaternion.LookRotation(rigidbody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "RPG")
            {
                rpgLoadAudioSource.PlayOneShot(rpgLoadAudioClip);
            }
        }

        public void CheckSocketAndFire()
        {
            IXRSelectInteractable objName = xRSocketInteractor.GetOldestInteractableSelected();
            var name = objName.transform.name;
            if (name.Contains("RPGWarheadGrab"))
            {
                objName.transform.gameObject.SetActive(false);
                Fire();
            }
        }
    }
}
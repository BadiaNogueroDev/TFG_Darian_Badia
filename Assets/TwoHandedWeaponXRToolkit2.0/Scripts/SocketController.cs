using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace SniperDemo
{
    public class SocketController : MonoBehaviour
    {
        public GameObject xrSocket;
        public XRSocketInteractor socket;
        public GameObject dummyMagazine;
        public AudioSource audioSourceReload;
        public AudioClip audioClipReload;
        private IXRSelectInteractable previousInteractable;

        public void PlayReloadSound()
        {
            IXRSelectInteractable objName = socket.GetOldestInteractableSelected();
            if (objName.transform.name.Contains("Magazine"))
            {
                audioSourceReload.PlayOneShot(audioClipReload);
            }
        }

        public void CheckIfGrabbedBySocket()
        {
            IXRSelectInteractable objName = socket.GetOldestInteractableSelected();
            if (objName.transform.name.Contains("AK47"))
            {
                Transform[] allChildren = objName.transform.GetComponentsInChildren<Transform>();
                foreach (Transform child in allChildren)
                {
                    if (child.name.Contains("Second Grab Point"))
                    {
                        child.gameObject.SetActive(false);
                    }
                }
            }
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
    }
}
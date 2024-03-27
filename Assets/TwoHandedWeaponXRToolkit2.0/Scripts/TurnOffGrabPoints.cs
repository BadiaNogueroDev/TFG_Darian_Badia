using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace SniperDemo
{
    public class TurnOffGrabPoints : MonoBehaviour
    {
        public XRSocketInteractor socket;
        
        public void CheckIfGrabbedBySocket()
        {
            IXRSelectInteractable objName = socket.GetOldestInteractableSelected();

            var childrenCount = objName?.transform.childCount;
            for (int i = 0; i < childrenCount; i++)
            {
                var child = objName.transform.GetChild(i);
                if (child.name.Contains("---Second Grab Point1---"))
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }
}
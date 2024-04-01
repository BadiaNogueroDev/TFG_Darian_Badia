using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace SniperDemo
{
    public class Hand : XRDirectInteractor
    {
        public GameObject LHand;
        public GameObject RHand;
        public bool shaderChange;
        
        public void GrabObject()
        {
            Manager.instance.data.ObjectsGrabbed++;
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD_Aim : MonoBehaviour
{
    public enum TYPE
    {
        Scope_One,
        Laser_One,
        Scope_Two,
        Laser_Two,
    }
    
    public TYPE HUDType;
    [SerializeField]private TMP_Dropdown hudSelection;

    public GameObject laser;
    public GameObject scope;
    public GameObject scopeSocket;
    
    public void SetDefaultParameters()
    {
        //Set dropdowns
        string[] hudTypeNames = Enum.GetNames(typeof(TYPE));
        List<string> hudTypeList = new List<string>(hudTypeNames);
        hudSelection.ClearOptions();
        hudSelection.AddOptions(hudTypeList);
    }
    
    public void SetType()
    {
        HUDType = (TYPE)hudSelection.value;
        switch (HUDType)
        {
            case TYPE.Scope_One:
            case TYPE.Scope_Two:
                laser.SetActive(false);
                scope.SetActive(true);
                scopeSocket.SetActive(true);
                break;
            case TYPE.Laser_One:
            case TYPE.Laser_Two:
                laser.SetActive(true);
                scope.SetActive(false);
                scopeSocket.SetActive(false);
                break;
        }
    }
}
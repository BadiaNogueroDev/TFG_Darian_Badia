using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD_Health : MonoBehaviour
{
    public enum TYPE
    {
        Meta,
        Diegetic,
        Diegetic_Meta,
    }
    
    public TYPE HUDType;
    [SerializeField]private TMP_Dropdown hudSelection;

    public GameObject watch;
    public GameObject vignette;
    
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
        HUDType = (TYPE)hudSelection.value;
        switch (HUDType)
        {
            case TYPE.Meta:
                watch.SetActive(false);
                vignette.SetActive(true);
                break;
            case TYPE.Diegetic:
                watch.SetActive(true);
                vignette.SetActive(false);
                break;
            case TYPE.Diegetic_Meta:
                watch.SetActive(true);
                vignette.SetActive(true);
                break;
        }
    }
}
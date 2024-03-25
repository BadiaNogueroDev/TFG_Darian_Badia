using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD_Ammo : MonoBehaviour
{
    public enum TYPE
    {
        None_One,
        Numeric_One,
        None_Two,
        Numeric_Two,
    }
    
    public TYPE HUDType;
    [SerializeField]private TMP_Dropdown hudSelection;

    public GameObject ammoInfo;
    
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
            case TYPE.None_One:
            case TYPE.None_Two:
                ammoInfo.SetActive(false);
                break;
            case TYPE.Numeric_One:
            case TYPE.Numeric_Two:
                ammoInfo.SetActive(true);
                break;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class HUD_Selection : MonoBehaviour
{
    //TO DO
    public enum TYPE
    {
        Spatial_One,
        Shader_One,
        Spatial_Two,
        Shader_Two,
    }
    
    public TYPE HUDType;
    [SerializeField]private TMP_Dropdown hudSelection;
    
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
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public HUD_Health hudHealth;
    public HUD_Ammo hudAmmo;
    public HUD_Aim hudAim;
    public HUD_Selection hudSelection;
    public Data data;
    
    public int selectedGroup = 0;
    public int selectedSubgroup = 0;
    
    public static Manager instance { get; private set; }

    private void Awake() 
    { 
        if (instance != null && instance != this) { Destroy(this); } 
        else { instance = this; }
        
        hudHealth = GetComponent<HUD_Health>();
        hudAmmo = GetComponent<HUD_Ammo>();
        hudAim = GetComponent<HUD_Aim>();
        hudSelection = GetComponent<HUD_Selection>();
    }

    private void Start()
    {
        hudHealth.SetDefaultParameters();
        hudAmmo.SetDefaultParameters();
        hudAim.SetDefaultParameters();
        hudSelection.SetDefaultParameters();
        Time.timeScale = 0;
    }

    public void StartPlaytest()
    {
        hudHealth.SetType();
        hudAmmo.SetType();
        hudAim.SetType();
        hudSelection.SetType();
        SetTestSubgroup();
        Time.timeScale = 1;
        
        StartCoroutine(FinishPlaytest());
    }

    public void SetTestGroup(int group)
    {
        selectedGroup = group;
    }

    public void SetTestSubgroup()
    {
        switch (selectedGroup)
        {
            case 1:
                selectedSubgroup = (int)hudHealth.HUDType + 1;
                break;
            case 2:
                selectedSubgroup = (int)hudAmmo.HUDType + 1;
                break;
            case 3:
                selectedSubgroup = (int)hudAim.HUDType + 1;
                break;
            case 4:
                selectedSubgroup = (int)hudSelection.HUDType + 1;
                break;
        }
    }

    IEnumerator FinishPlaytest()
    {
        yield return new WaitForSeconds(120);
        //FinishPlaytest
        //Print data
        yield return null;
    }
}
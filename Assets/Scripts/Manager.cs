using System;
using System.Collections;
using SniperDemo;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Manager : MonoBehaviour
{
    public HealthSystem playerHealth;
    public EnemyController[] enemies;

    public TMP_InputField playtesterName;
    public HUD_Health hudHealth;
    public HUD_Ammo hudAmmo;
    public HUD_Aim hudAim;
    public HUD_Selection hudSelection;
    public Data data;
    
    public int selectedGroup = 0;
    public int selectedSubgroup = 0;
    public float totalTimer;
    private float timer;
    private bool playing;

    private string DataFileName = "";
    public GameObject finalPanel;
    public TMP_Text timerText;
    
    public static Manager instance { get; private set; }

    private void Awake() 
    { 
        if (instance != null && instance != this) { Destroy(this); } 
        else { instance = this; }
        
        hudHealth = GetComponent<HUD_Health>();
        hudAmmo = GetComponent<HUD_Ammo>();
        hudAim = GetComponent<HUD_Aim>();
        hudSelection = GetComponent<HUD_Selection>();
        finalPanel.SetActive(false);
    }

    private void Start()
    {
        hudHealth.SetDefaultParameters();
        hudAmmo.SetDefaultParameters();
        hudAim.SetDefaultParameters();
        hudSelection.SetDefaultParameters();
        timer = totalTimer;
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (playing)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F1");
            
            if (timer <= 0.0f)
            {
                playing = false;
            }
        }
    }

    public void ResetGame()
    {
        enemies = FindObjectsOfType<EnemyController>();
        for (int i = 0; i < enemies.Length; i++)
        {
            data.EnemiesKilled--;
            enemies[i].Die();
        }
        playerHealth.Revive();
    }

    public void StartPlaytest()
    {
        hudHealth.SetType();
        hudAmmo.SetType();
        hudAim.SetType();
        hudSelection.SetType();
        SetTestSubgroup();
        StartCoroutine(FinishPlaytest());
        DataFileName = Application.dataPath + $"/Data/Data from {playtesterName.text} {selectedGroup}-{selectedSubgroup}.csv";
        Time.timeScale = 1;
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
        playing = true;
        yield return new WaitForSeconds(totalTimer);
        string HUD_Version = selectedGroup.ToString() + "." + selectedSubgroup.ToString();
        WriteCSV(HUD_Version);
        finalPanel.SetActive(true);
        Time.timeScale = 0;
        yield return null;
    }

    public void RestartPlaytest()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ClosePlaytest()
    {
        Application.Quit();
    }

    public void WriteCSV(string version)
    {
        TextWriter tw = new StreamWriter(DataFileName, false);
        tw.WriteLine("HUD Version, Playtester, Hits received, Magazines changed, Shots fired, Enemies killed, Objects grabbed");
        tw.Close();
        
        tw = new StreamWriter(DataFileName, true);
        tw.WriteLine(version + "," + 
                     playtesterName.text + "," + 
                     data.HitsReceived + "," + 
                     data.MagazineChanged + "," +
                     data.ShotsFired + "," + 
                     data.EnemiesKilled + "," + 
                     data.ObjectsGrabbed + ",");
        tw.Close();
    }
}
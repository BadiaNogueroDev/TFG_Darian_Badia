using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class HealthSystem : MonoBehaviour
{
    public Image healthBar;
    //FALTA EL TEMA VIGNETTE
    
    [SerializeField] private float vignetteAperture;
    [SerializeField] private float vignetteTransitionTime;
    
    private float currentHealth;
    private float maxHealth = 100;

    private void Start()
    {
        Revive();
    }

    public void GetDamage(float damage)
    {
        Manager.instance.data.HitsReceived++;
        
        currentHealth -= damage;
        if (currentHealth <= 0) Manager.instance.ResetGame();
        UpdateHUD();
    }

    public void Revive()
    {
        currentHealth = maxHealth;
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        switch (Manager.instance.hudHealth.HUDType)
        {
            case HUD_Health.TYPE.Diegetic:
                healthBar.fillAmount = currentHealth / maxHealth;
                break;
            case HUD_Health.TYPE.Meta:
                StartCoroutine(Vignette());
                break;
            case HUD_Health.TYPE.Diegetic_Meta:
                StartCoroutine(Vignette());
                healthBar.fillAmount = currentHealth / maxHealth;
                break;
        }
    }

    IEnumerator Vignette()
    {
        VignetteParameters param = new VignetteParameters();
        float timer = 0;
        
        while (timer < vignetteTransitionTime)
        {
            param.apertureSize = Mathf.Lerp(0, vignetteAperture, timer / vignetteTransitionTime);
            timer += Time.deltaTime;
        }
        
        timer = 0;
        
        while (timer < vignetteTransitionTime)
        {
            param.apertureSize = Mathf.Lerp(vignetteAperture, 0, timer / vignetteTransitionTime);
            timer += Time.deltaTime;
        }
        
        yield return null;
    }
}
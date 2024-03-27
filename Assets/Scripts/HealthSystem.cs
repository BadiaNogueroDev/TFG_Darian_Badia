using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class HealthSystem : MonoBehaviour
{
    public Image healthBar;
    public TunnelingVignetteController vignette;
    public LocomotionVignetteProvider locomotionVignetteProvider = new LocomotionVignetteProvider();
    
    [SerializeField] private float vignetteAperture;
    [SerializeField] private float vignetteTransitionTime;
    
    private float currentHealth;
    private float maxHealth = 100;

    private void Start()
    {
        currentHealth = maxHealth;
        vignette.locomotionVignetteProviders.Add(locomotionVignetteProvider);
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
        healthBar.fillAmount = currentHealth / maxHealth;
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
        vignette.BeginTunnelingVignette(locomotionVignetteProvider);
        yield return new WaitForSeconds(0.3f);
        vignette.EndTunnelingVignette(locomotionVignetteProvider);
    }
}
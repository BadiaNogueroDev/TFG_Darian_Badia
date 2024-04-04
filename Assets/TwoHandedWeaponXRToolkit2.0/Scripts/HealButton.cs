using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealButton : MonoBehaviour
{
    private float timer;
    [SerializeField] private float timerMax;
    [SerializeField] private bool canBePressed;
    [SerializeField] private Color canBePressedColor;
    [SerializeField] private Color canNotBePressedColor;
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Animator anim;

    private void Start()
    {
        timer = 0;
        canBePressed = false;
        mesh.material.color = canNotBePressedColor;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timerMax)
        {
            mesh.material.color = canBePressedColor;
            canBePressed = true;
        }
    }

    public void HealPlayer()
    {
        timer = 0;
        canBePressed = false;
        anim.SetTrigger("Press");
        mesh.material.color = canNotBePressedColor;
        Manager.instance.playerHealth.Heal(10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand") && canBePressed)
        {
            HealPlayer();
        }
    }
}

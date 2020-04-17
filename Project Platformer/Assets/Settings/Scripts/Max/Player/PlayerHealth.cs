﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;
using DG.Tweening;
using Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Damagable
{
    public float oxygenLvl;

    public float damageCooldown;
    private float oxygenDamageAmount;

    [Header("Sliders")]
    public Slider oxygenSlider;
    public Slider healthSlider;

    CinemachineVirtualCamera vcam;
    CinemachineBasicMultiChannelPerlin noise;


    [SerializeField]private bool isTakingDamage;

    PauseMenu pauseMenu;

    [Header("audio")]
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioSource oxygenLoss;

    [Header("Color for player flash")]
    [SerializeField] private Color color = Color.white;
    [SerializeField] private Color color2 = Color.white;

    private void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();

        vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    private void Update()
    {
        //StartCoroutine(OxygenTime(100, 1, 0.05f));

        LoseOxygen();

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        oxygenSlider.value = oxygenLvl;
        healthSlider.value = health;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlantStem"))
        {
            if (health > 0)
            {
                if (isTakingDamage == false)
                {
                    PlayerDamage(5f);

                    audio.Play();
                    StartCoroutine(PlayerFlash(0.1f));
                    StartCoroutine(CameraShake(1.5f, 1.5f));
                    gameObject.tag = "NoDamage";
                }
            }
            if (health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (collision.CompareTag("oxygenTank"))
        {
            AddOxygen(25f);
            Destroy(collision.gameObject);
        }

        if(collision.CompareTag("DeathBarrier"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Spikes"))
        {
            audio.Play();
            PlayerDamage(3f);
        }
        if (collision.CompareTag("DeathBarrier"))
        {
            for (int i = 0; i < 60; i++)
            {
                PlayerDamage(100f);
            }
        }
    }

    private IEnumerator CameraShake(float amplitudeGain, float frequencyGain)
    {
        isTakingDamage = true;
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;

        yield return new WaitForSeconds(2f);

        isTakingDamage = false;
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;
    }

    public void LoseOxygen()
    {
        if (oxygenLvl <= 0)
        {
            oxygenDamageAmount = 0;
            StartCoroutine(BloodLossTime(1000, 1, 0.05f));
        }
        else if(oxygenLvl > 0)
        {
            oxygenDamageAmount = 0.03f;
            oxygenLvl -= oxygenDamageAmount;
        }
    }

    private IEnumerator PlayerFlash(float cooldown)
    {
        for (int i = 0; i < 8; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().color = color;

            yield return new WaitForSeconds(cooldown);

            gameObject.GetComponent<SpriteRenderer>().color = color2;

            yield return new WaitForSeconds(cooldown);
        }
    }

    public void AddOxygen(float amountOfOxygen)
    {
        oxygenLvl += amountOfOxygen;
    }
}

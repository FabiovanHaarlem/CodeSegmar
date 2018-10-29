using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Opsive.ThirdPersonController.Wrappers;

public class PlayerLightResources : MonoBehaviour
{
    [SerializeField]
    private GameObject m_UpperLight;
    private Renderer m_CurrentLightRenderer;
    private Inventory m_Ammo;
    private CharacterHealth m_Health;
    [SerializeField]
    private AudioSource[] m_AudioSounds;

    private float m_SwitchValue;
    private float m_GreenValue;
    private float m_RedValue;
    [SerializeField]
    private float m_LightCharge;

    private int m_CurrentLightIndex;
    private int m_CurrentAmmo;

    private bool m_GodMode;
    private bool m_Charging;
    private bool m_SoundPlayed;

    public float GetLightCharge
    {
        get { return m_LightCharge; }
        set { m_LightCharge = value; }
    }

    public bool GetCharging
    {
        get { return m_Charging; }
        set { m_Charging = value; }
    }

    private void Start ()
    {
        m_CurrentLightRenderer = m_UpperLight.GetComponent<Renderer>();
        m_Ammo = GetComponent<Inventory>();
        m_Health = GetComponent<CharacterHealth>();

        m_LightCharge = 23;
        m_SwitchValue = 0;
        m_CurrentLightIndex = 0;
        m_RedValue = 0;
        m_GreenValue = 0;
        m_GodMode = false;
        m_Charging = false;
        m_SoundPlayed = false;
    }

    private void Update()
    {

        m_GreenValue = 2.55f / 100f * m_LightCharge;
        m_RedValue = 2.55f / 100f * m_SwitchValue;

        if (m_GreenValue >= 255)
        {
            m_GreenValue = 255;
        }
        else if (m_GreenValue <= 0)
        {
            m_GreenValue = 0;
        }

        if (m_RedValue >= 255)
        {
            m_RedValue = 255;
        }
        else if (m_RedValue <= 0)
        {
            m_RedValue = 0;
        }

        if (m_LightCharge <= 0)
        {
            m_LightCharge = 0;
        }

        m_CurrentLightRenderer.material.color = new Color(m_RedValue, m_GreenValue, 0);
        m_CurrentLightRenderer.material.SetColor("_EmissionColor", new Color(m_RedValue / 2f, m_GreenValue / 2f, 0));
        m_CurrentLightRenderer.gameObject.GetComponent<Light>().color = new Color(m_RedValue, m_GreenValue, 0);

        if (Input.GetKeyDown(KeyCode.F1))
        {
            m_GodMode = !m_GodMode;

            if (m_GodMode)
            {
                m_Ammo.SetItemCount(m_Ammo.GetCurrentItem(typeof(PrimaryItemType)).ItemType, int.MaxValue, int.MaxValue);
                m_Health.Invincible = true;
            }
            else if (!m_GodMode)
            {
                m_Ammo.SetItemCount(m_Ammo.GetCurrentItem(typeof(PrimaryItemType)).ItemType, 50 , 250);
                m_Health.Invincible = false;
            }
        }
        
        if (m_LightCharge == 50 && !m_SoundPlayed)
        {
            m_AudioSounds[1].Play();
            m_SoundPlayed = true;
        }
        else if (m_LightCharge != 50)
        {
            m_SoundPlayed = false;
        }

        if (!m_Charging && m_LightCharge == 20)
        {
            m_AudioSounds[0].Play();
        }

        
    }

    public void UseEnergy(float energyUse)
    {
        if (!m_Charging)
        {
            m_LightCharge -= energyUse;
            m_SwitchValue += energyUse;
            if (m_LightCharge <= 0)
            {
                m_LightCharge = 0;
                m_SwitchValue = 100;
            }
        }
    }


    public void ChargeLight(float powerOutput)
    {
        m_LightCharge += powerOutput;
    }
}

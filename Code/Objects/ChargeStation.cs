using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeStation : MonoBehaviour
{
    [SerializeField]
    private PlayerLightResources m_PlayerResources;
    private ParticleSystem m_ParticleSystem;
    [SerializeField]
    private AudioSource[] m_AudioClips;

    [SerializeField]
    private int m_PowerInStation;

    private float m_ChargeTimer;

    private bool m_SoundPlays;

    void Start()
    {
        m_ParticleSystem = GetComponent<ParticleSystem>();

        m_PowerInStation = 80;
        m_ChargeTimer = 0;
        m_SoundPlays = false;
    }


    void Update()
    {
        if (m_PowerInStation <= 0)
        {
            m_ParticleSystem.Stop();
            m_AudioClips[0].Stop();
            m_PowerInStation = 0;
            if (!m_SoundPlays)
            {
                m_AudioClips[2].Play();
                m_SoundPlays = true;
            }     
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_ChargeTimer += Time.deltaTime;
            if (m_PowerInStation >= 1 && m_ChargeTimer >= 0.05f)
            {
                if (other.GetComponent<PlayerLightResources>().GetLightCharge < 100)
                {
                    other.GetComponent<PlayerLightResources>().ChargeLight(1f);
                    m_PowerInStation -= 1;
                    m_ChargeTimer = 0;
                }
            }
            else if (m_PowerInStation <= 0)
            {
                m_ParticleSystem.Stop();
                m_AudioClips[0].Stop();
                m_PowerInStation = 0;
                if (!m_SoundPlays)
                {
                    m_AudioClips[2].Play();
                    m_SoundPlays = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_PowerInStation >= 1)
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerLightResources>().GetCharging = true;
                m_AudioClips[0].Play();
                m_AudioClips[1].Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerLightResources>().GetCharging = false;
            m_AudioClips[0].Stop();
        }
    }
}

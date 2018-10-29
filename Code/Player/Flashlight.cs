using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {

    [SerializeField]
    private GameObject m_Player;
    private Light m_LightComponent;   

    [SerializeField]
    private int m_EnergyUse;
    [SerializeField]
    private float m_LightTimer;
    private float m_CurrentLightTimer;
    private float m_LightOnOffTimer;
    private float m_LightStrengthe;

    private bool m_LightOn;

    private void Start()
    {
        m_LightComponent = GetComponent<Light>();

        m_LightTimer = 0;
        m_LightOnOffTimer = 0;
        m_LightOn = true;
        m_EnergyUse = 1;
        m_LightTimer = 1;
    }

    private void Update()
    {
        m_CurrentLightTimer += Time.deltaTime;
        m_LightOnOffTimer += Time.deltaTime;

        if (m_CurrentLightTimer > m_LightTimer && m_LightOn)
        {
            m_Player.GetComponent<PlayerLightResources>().UseEnergy(m_EnergyUse);
            m_CurrentLightTimer = 0;
        }

        if (Input.GetKey(KeyCode.F) && m_LightOnOffTimer >= 1)
        {
            if (m_LightOn)
            {
                m_LightOn = false;
            }
            else if (!m_LightOn)
            {
                m_LightOn = true;
            }
        }

        m_LightComponent.intensity = m_Player.GetComponent<PlayerLightResources>().GetLightCharge / 100;
    }
}

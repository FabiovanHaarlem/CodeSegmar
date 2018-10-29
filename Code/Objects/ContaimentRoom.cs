using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContaimentRoom : MonoBehaviour
{
    //2 gaat open 1 gaat dicht
    [SerializeField]
    private GameObject m_LeftDoorExit;
    [SerializeField]
    private GameObject m_RightDoorExit;
    [SerializeField]
    private GameObject m_LeftDoorEntrance;
    [SerializeField]
    private GameObject m_RightDoorEntrance;
    [SerializeField]
    private Transform m_ExitLeftOpenDoorPosition;
    [SerializeField]
    private Transform m_ExitRightOpenDoorPosition;
    [SerializeField]
    private Transform m_ExitLeftClosedDoorPosition;
    [SerializeField]
    private Transform m_ExitRightClosedDoorPosition;
    [SerializeField]
    private Transform m_EntranceLeftOpenDoorPosition;
    [SerializeField]
    private Transform m_EntranceRightOpenDoorPosition;
    [SerializeField]
    private Transform m_EntranceLeftClosedDoorPosition;
    [SerializeField]
    private Transform m_EntranceRightClosedDoorPosition;
    [SerializeField]
    private GameObject[] m_DoorLights;
    [SerializeField]
    private List<GameObject> m_GasStations;
    [SerializeField]
    private AudioSource[] m_Sounds;

    private float m_DoorOpenStartWaveTime;
    private float m_CurrentDoorStartWaveTime;
    private float m_DoorWaveStopTime;
    private float m_CurrentDoorOpenWaveStopTime;
    private float m_WaitTime;

    private bool m_Locked;

    void Start()
    {
        m_DoorOpenStartWaveTime = 10f;
        m_DoorWaveStopTime = 1f;
        m_CurrentDoorOpenWaveStopTime = 0f;
        m_WaitTime = 3;
        m_Locked = false;

        for (int i = 0; i < m_DoorLights.Length; i++)
        {
            m_DoorLights[i].GetComponent<Renderer>().material.color = Color.green;
            m_DoorLights[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
            m_DoorLights[i].GetComponent<Light>().color = Color.green;
        }

        for (int i = 0; i < m_GasStations.Count; i++)
        {
            m_GasStations[i].SetActive(false);
        }
    }


    void Update()
    {
        if (m_Locked && m_WaitTime <= 0)
        {
            m_Sounds[1].Play();
            m_Locked = false;
            m_WaitTime = 4;
            m_CurrentDoorOpenWaveStopTime = 0;

            for (int i = 0; i < m_DoorLights.Length; i++)
            {
                m_DoorLights[i].GetComponent<Renderer>().material.color = Color.green;
                m_DoorLights[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                m_DoorLights[i].GetComponent<Light>().color = Color.green;
            }

            for (int i = 0; i < m_GasStations.Count; i++)
            {
                m_GasStations[i].SetActive(false);
            }    
        }

        if (m_Locked)
        {
            m_WaitTime -= Time.deltaTime;
        }

        if (m_Locked)
        {
            if (m_CurrentDoorOpenWaveStopTime < m_DoorWaveStopTime)
            {
                m_CurrentDoorOpenWaveStopTime += Time.deltaTime;
            }
            m_LeftDoorEntrance.transform.position = Vector3.Lerp(m_EntranceLeftOpenDoorPosition.position, m_EntranceLeftClosedDoorPosition.position, m_CurrentDoorOpenWaveStopTime / m_DoorWaveStopTime);
            m_RightDoorEntrance.transform.position = Vector3.Lerp(m_EntranceRightOpenDoorPosition.position, m_EntranceRightClosedDoorPosition.position, m_CurrentDoorOpenWaveStopTime / m_DoorWaveStopTime);
            m_LeftDoorExit.transform.position = Vector3.Lerp(m_ExitLeftOpenDoorPosition.position, m_ExitLeftClosedDoorPosition.position, m_CurrentDoorOpenWaveStopTime / m_DoorWaveStopTime);
            m_RightDoorExit.transform.position = Vector3.Lerp(m_ExitRightOpenDoorPosition.position, m_ExitRightClosedDoorPosition.position, m_CurrentDoorOpenWaveStopTime / m_DoorWaveStopTime);
        }
        else if (!m_Locked)
        {
            if (m_CurrentDoorOpenWaveStopTime < m_DoorOpenStartWaveTime)
            {
                m_CurrentDoorOpenWaveStopTime += Time.deltaTime;
            }
            m_LeftDoorEntrance.transform.position = Vector3.Lerp(m_EntranceLeftClosedDoorPosition.position, m_EntranceLeftOpenDoorPosition.position, m_CurrentDoorOpenWaveStopTime / m_DoorWaveStopTime);
            m_RightDoorEntrance.transform.position = Vector3.Lerp(m_EntranceRightClosedDoorPosition.position, m_EntranceRightOpenDoorPosition.position, m_CurrentDoorOpenWaveStopTime / m_DoorWaveStopTime);
            m_LeftDoorExit.transform.position = Vector3.Lerp(m_ExitLeftClosedDoorPosition.position, m_ExitLeftOpenDoorPosition.position, m_CurrentDoorOpenWaveStopTime / m_DoorWaveStopTime);
            m_RightDoorExit.transform.position = Vector3.Lerp(m_ExitRightClosedDoorPosition.position, m_ExitRightOpenDoorPosition.position, m_CurrentDoorOpenWaveStopTime / m_DoorWaveStopTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!m_Locked)
            {
                m_Sounds[0].Play();
                m_Sounds[1].Play();
                for (int i = 0; i < m_DoorLights.Length; i++)
                {
                    m_DoorLights[i].GetComponent<Renderer>().material.color = Color.red;
                    m_DoorLights[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
                    m_DoorLights[i].GetComponent<Light>().color = Color.red;
                }

                for (int i = 0; i < m_GasStations.Count; i++)
                {
                    m_GasStations[i].SetActive(true);
                }

                m_CurrentDoorOpenWaveStopTime = 0;
                m_Locked = true;
                m_WaitTime = 3;
            } 
        }
    }
}

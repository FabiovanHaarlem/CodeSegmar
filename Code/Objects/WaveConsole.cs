using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveConsole : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_DoorLights1;
    [SerializeField]
    private GameObject[] m_DoorLights2;
    [SerializeField]
    private GameObject m_LeftDoorWave;
    [SerializeField]
    private GameObject m_RightDoorWave;
    [SerializeField]
    private GameObject m_LeftDoorEntrance;
    [SerializeField]
    private GameObject m_RightDoorEntrance;
    [SerializeField]
    private List<Transform> m_SpawnLocation;
    [SerializeField]
    private Transform m_LeftDoorOpenPositionWave1;
    [SerializeField]
    private Transform m_RightDoorOpenPositionWave2;
    [SerializeField]
    private Transform m_LeftDoorClosedPositionEntrance1;
    [SerializeField]
    private Transform m_RightDoorClosedPositionEntrance2;
    [SerializeField]
    private Transform m_LeftDoorOpenPositionEntrance1;
    [SerializeField]
    private Transform m_RightDoorOpenPositionEntrance2;

    private Transform m_LeftDoorClosedPosition1;
    private Transform m_RightDoorClosedPosition1;
    private Transform m_LeftDoorOpenPosition2;
    private Transform m_RightDoorOpenPosition2;

    private AudioSource m_WarningSound;

    private float m_DoorOpenStartWaveTime;
    private float m_CurrentDoorStartWaveTime;
    private float m_DoorWaveStopTime;
    private float m_CurrentDoorOpenWaveStopTime;
    private float m_WaveDuration;
    private float m_SpawnWaveTimer;

    [SerializeField]
    private bool m_OnlyWaves;
    private bool m_StartWaves;
    private bool m_StopWaves;

    void Start()
    {
        m_WarningSound = GetComponent<AudioSource>();

        m_DoorOpenStartWaveTime = 10f;
        m_DoorWaveStopTime = 5f;
        m_CurrentDoorOpenWaveStopTime = 0f;
        m_SpawnWaveTimer = 5f;
        m_WaveDuration = 30f;
        m_StopWaves = false;
        m_StartWaves = false;

        if (!m_OnlyWaves)
        {
            m_LeftDoorClosedPosition1 = m_LeftDoorWave.GetComponent<Transform>();
            m_RightDoorClosedPosition1 = m_RightDoorWave.GetComponent<Transform>();
            m_LeftDoorOpenPosition2 = m_LeftDoorEntrance.GetComponent<Transform>();
            m_RightDoorOpenPosition2 = m_RightDoorEntrance.GetComponent<Transform>();

            for (int i = 0; i < m_DoorLights1.Length; i++)
            {
                m_DoorLights1[i].GetComponent<Renderer>().material.color = Color.green;
                m_DoorLights1[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                m_DoorLights1[i].GetComponent<Light>().color = Color.green;
            }

            for (int i = 0; i < m_DoorLights2.Length; i++)
            {
                m_DoorLights2[i].GetComponent<Renderer>().material.color = Color.red;
                m_DoorLights2[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
                m_DoorLights2[i].GetComponent<Light>().color = Color.red;
            }
        }
    }


    void Update()
    {
        if (!m_OnlyWaves)
        {
            if (m_StopWaves)
            {
                if (m_CurrentDoorOpenWaveStopTime < m_DoorWaveStopTime)
                {
                    m_CurrentDoorOpenWaveStopTime += Time.deltaTime;
                }
                m_LeftDoorEntrance.transform.position = Vector3.Lerp(m_LeftDoorClosedPositionEntrance1.position, m_LeftDoorOpenPositionEntrance1.position, m_CurrentDoorOpenWaveStopTime / m_DoorWaveStopTime);
                m_RightDoorEntrance.transform.position = Vector3.Lerp(m_RightDoorClosedPositionEntrance2.position, m_RightDoorOpenPositionEntrance2.position, m_CurrentDoorOpenWaveStopTime / m_DoorWaveStopTime);
            }
            else if (m_StartWaves)
            {
                if (m_CurrentDoorOpenWaveStopTime < m_DoorOpenStartWaveTime)
                {
                    m_CurrentDoorOpenWaveStopTime += Time.deltaTime;
                }
                m_LeftDoorEntrance.transform.position = Vector3.Lerp(m_LeftDoorOpenPosition2.position, m_LeftDoorClosedPositionEntrance1.position, m_CurrentDoorOpenWaveStopTime / m_DoorOpenStartWaveTime);
                m_RightDoorEntrance.transform.position = Vector3.Lerp(m_RightDoorOpenPosition2.position, m_RightDoorClosedPositionEntrance2.position, m_CurrentDoorOpenWaveStopTime / m_DoorOpenStartWaveTime);
                m_LeftDoorWave.transform.position = Vector3.Lerp(m_LeftDoorClosedPosition1.position, m_LeftDoorOpenPositionWave1.position, m_CurrentDoorOpenWaveStopTime / m_DoorOpenStartWaveTime);
                m_RightDoorWave.transform.position = Vector3.Lerp(m_RightDoorClosedPosition1.position, m_RightDoorOpenPositionWave2.position, m_CurrentDoorOpenWaveStopTime / m_DoorOpenStartWaveTime);
                m_WaveDuration -= Time.deltaTime;

                if (m_StartWaves)
                {
                    if (m_WaveDuration >= 0f)
                    {
                        m_SpawnWaveTimer += Time.deltaTime;

                        if (m_SpawnWaveTimer >= 5f)
                        {
                            for (int i = 0; i < m_SpawnLocation.Count; i++)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    GameObject enemy = Instantiate(Resources.Load("Prefabs\\EnemyMutant"), m_SpawnLocation[i].position, m_SpawnLocation[i].rotation) as GameObject;
                                }
                                m_SpawnWaveTimer = 0f;
                            }
                        }
                    }
                }

            }
        }
        else if (m_OnlyWaves)
        {
            if (m_StartWaves && !m_StopWaves)
            {
                m_WaveDuration -= Time.deltaTime;
                if (m_WaveDuration >= 0f)
                {
                    m_SpawnWaveTimer += Time.deltaTime;

                    if (m_SpawnWaveTimer >= 5f)
                    {
                        for (int i = 0; i < m_SpawnLocation.Count; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                GameObject enemy = Instantiate(Resources.Load("Prefabs\\EnemyMutant"), m_SpawnLocation[i].position, m_SpawnLocation[i].rotation) as GameObject;
                            }
                            m_SpawnWaveTimer = 0f;
                        }
                    }
                }
            }
        }



        if (m_WaveDuration <= 0)
        {
            if (!m_StopWaves)
            {
                m_StartWaves = false;
                m_StopWaves = true;
                m_CurrentDoorOpenWaveStopTime = 0f;

                for (int i = 0; i < m_DoorLights1.Length; i++)
                {
                    m_DoorLights1[i].GetComponent<Renderer>().material.color = Color.green;
                    m_DoorLights1[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                    m_DoorLights1[i].GetComponent<Light>().color = Color.green;
                }
            }
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (m_StopWaves == false && m_StartWaves == false)
        {
            if (other.gameObject.tag == "Player")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    m_WarningSound.Play();

                    for (int i = 0; i < m_DoorLights1.Length; i++)
                    {
                        m_DoorLights1[i].GetComponent<Renderer>().material.color = Color.red;
                        m_DoorLights1[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
                        m_DoorLights1[i].GetComponent<Light>().color = Color.red;

                        m_DoorLights2[i].GetComponent<Renderer>().material.color = Color.green;
                        m_DoorLights2[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                        m_DoorLights2[i].GetComponent<Light>().color = Color.green;

                        GetComponent<Renderer>().material.color = Color.green;
                        GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                        GetComponent<Light>().color = Color.green;           
                    }
                    m_StartWaves = true;
                }

            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{

    [SerializeField]
    private GameObject m_Door;
    [SerializeField]
    private GameObject[] m_DoorLights;
    [SerializeField]
    private Transform m_OpenOrClosePosition;
    [SerializeField]
    private Transform m_ElevatorGoToPosition;
    private Vector3 m_ElevatorStartPosition;
    private Vector3 m_StartPosition;
    private AudioSource m_DoorSound;
    [SerializeField]
    private PlayerLightResources m_LightResources;
    [SerializeField]
    private Intro m_IntroScript;


    [SerializeField]
    private float m_DoorOpenTime;
    private float m_CurrentDoorOpenTime;
    private float m_ElevatorMoveTime;
    private float m_ElevatorCurrentMoveTime;
    private float m_OpenDoorTimer;
    private float m_EndTimer;


    [SerializeField]
    private bool m_MoveDown;
    private bool m_OpenDoor;
    private bool m_CloseDoor;
    private bool m_SoundPlayed;

    [SerializeField]
    private string m_LoadSceneName;

    void Start()
    {
        m_DoorSound = GetComponent<AudioSource>();

        m_DoorOpenTime = 1f;
        m_CurrentDoorOpenTime = 0f;
        m_ElevatorMoveTime = 60f;
        m_ElevatorCurrentMoveTime = 0f;
        m_OpenDoorTimer = 61f;
        m_EndTimer = 6f;
        m_OpenDoor = false;
        m_SoundPlayed = false;

        m_StartPosition = m_Door.transform.position;
        m_ElevatorStartPosition = transform.position;

        if (m_MoveDown)
        {
            for (int i = 0; i < 2; i++)
            {
                m_DoorLights[i].GetComponent<Renderer>().material.color = Color.red;
                m_DoorLights[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
                m_DoorLights[i].GetComponent<Light>().color = Color.red;
            }
        }
        else if (!m_MoveDown)
        {
            for (int i = 0; i < 2; i++)
            {
                m_DoorLights[i].GetComponent<Renderer>().material.color = Color.green;
                m_DoorLights[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                m_DoorLights[i].GetComponent<Light>().color = Color.green;
            }
        }
    }


    void Update()
    {
        MoveDoorsAndElevators();
        m_OpenDoorTimer -= Time.deltaTime;

        if (m_MoveDown && m_OpenDoor != true)
        {
            if (m_OpenDoorTimer <= 0 && m_OpenDoor != true)
            {
                m_StartPosition = m_Door.transform.position;
                m_OpenDoor = true;

                if (!m_SoundPlayed)
                {
                    m_DoorSound.Play();
                    m_SoundPlayed = false;
                }

                for (int i = 0; i < 2; i++)
                {
                    m_DoorLights[i].GetComponent<Renderer>().material.color = Color.green;
                    m_DoorLights[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                    m_DoorLights[i].GetComponent<Light>().color = Color.green;
                }
            }
        }


        if (m_EndTimer <= 0)
        {
            SceneManager.LoadScene(m_LoadSceneName);
        }

    }

    private void MoveDoorsAndElevators()
    {
        if (m_OpenDoor)
        {
            if (m_CurrentDoorOpenTime < m_DoorOpenTime)
            {
                m_CurrentDoorOpenTime += Time.deltaTime;
            }
            m_Door.transform.position = Vector3.Lerp(m_StartPosition, m_OpenOrClosePosition.position, m_CurrentDoorOpenTime / m_DoorOpenTime);
        }
        else if (m_CloseDoor)
        {
            if (m_CurrentDoorOpenTime < m_DoorOpenTime)
            {
                m_CurrentDoorOpenTime += Time.deltaTime;
            }
            m_Door.transform.position = Vector3.Lerp(m_StartPosition, m_OpenOrClosePosition.position, m_CurrentDoorOpenTime / m_DoorOpenTime);

        }

        if (m_MoveDown)
        {
            if (m_ElevatorCurrentMoveTime < m_ElevatorMoveTime)
            {
                m_ElevatorCurrentMoveTime += Time.deltaTime;
            }
            transform.position = Vector3.Lerp(m_ElevatorStartPosition, m_ElevatorGoToPosition.position, m_ElevatorCurrentMoveTime / m_ElevatorMoveTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!m_MoveDown)
            {
                m_CloseDoor = true;

                if (!m_SoundPlayed)
                {
                    m_DoorSound.Play();
                    m_SoundPlayed = true;
                }

                if (m_CurrentDoorOpenTime >= 1)
                {
                    if (m_ElevatorCurrentMoveTime < m_ElevatorMoveTime)
                    {
                        m_ElevatorCurrentMoveTime += Time.deltaTime;
                    }
                    Debug.Log("End");
                    m_EndTimer -= Time.deltaTime;
                    m_CloseDoor = false;
                    transform.position = Vector3.Lerp(m_ElevatorStartPosition, m_ElevatorGoToPosition.position, m_ElevatorCurrentMoveTime / m_ElevatorMoveTime);
                    m_Door.transform.position = m_OpenOrClosePosition.position;
                }

                for (int i = 0; i < 2; i++)
                {
                    m_DoorLights[i].GetComponent<Renderer>().material.color = Color.red;
                    m_DoorLights[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
                    m_DoorLights[i].GetComponent<Light>().color = Color.red;
                }
            }
            else if (m_MoveDown)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    m_LightResources.GetLightCharge = 23;

                    if (Input.GetKey(KeyCode.Tab))
                    {
                        Time.timeScale = 4;
                    }
                    else if (Input.GetKeyUp(KeyCode.Tab))
                    {
                        Time.timeScale = 1;
                        m_IntroScript.StopAllAudio();
                    }
                }
            }
        }
    }
}

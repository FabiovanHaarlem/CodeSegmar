using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleDoorControle : MonoBehaviour 
{
    [SerializeField]
    private GameObject m_LeftDoor;
    [SerializeField]
    private GameObject m_RightDoor;
    [SerializeField]
    private GameObject[] m_DoorLights;
    [SerializeField]
    private Transform m_LeftDoorOpenPosition;
    [SerializeField]
    private Transform m_RightDoorOpenPosition;
    private Transform m_LeftDoorClosedPosition;
    private Transform m_RightDoorClosedPosition;
    [SerializeField]
    private AudioSource[] m_Sounds;

    private float m_DoorOpenTime;
    private float m_CurrentDoorOpenTime;
    private float m_DistanceRightDoor;
    private float m_DistanceLeftDoor;

    private bool m_OpenDoor;
    private bool m_DoorLock;
    [SerializeField]
    private bool m_EnemysBehindDoor;

    void Start()
    {
        m_LeftDoorClosedPosition = m_LeftDoor.GetComponent<Transform>();
        m_RightDoorClosedPosition = m_RightDoor.GetComponent<Transform>();

        m_DoorOpenTime = 20f;
        m_CurrentDoorOpenTime = 0;
        m_DoorLock = true;

        for (int i = 0; i < 2; i++)
        {
            m_DoorLights[i].GetComponent<Renderer>().material.color = Color.red;
            m_DoorLights[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
            m_DoorLights[i].GetComponent<Light>().color = Color.red;
        }
    }


    void Update()
    {
        if (!m_DoorLock)
        {
            if (m_OpenDoor)
            {
                if (m_CurrentDoorOpenTime < m_DoorOpenTime)
                {
                    m_CurrentDoorOpenTime += Time.deltaTime;
                }
                m_LeftDoor.transform.position = Vector3.Lerp(m_LeftDoorClosedPosition.position, m_LeftDoorOpenPosition.position, m_CurrentDoorOpenTime / m_DoorOpenTime);
                m_RightDoor.transform.position = Vector3.Lerp(m_RightDoorClosedPosition.position, m_RightDoorOpenPosition.position, m_CurrentDoorOpenTime / m_DoorOpenTime);
            }
        }
    }

    public void UnlockDoor()
    {
        if (m_DoorLock)
        {
            m_DoorLock = false;

            for (int i = 0; i < 2; i++)
            {
                m_DoorLights[i].GetComponent<Renderer>().material.color = Color.blue;
                m_DoorLights[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.blue);
                m_DoorLights[i].GetComponent<Light>().color = Color.blue;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!m_DoorLock)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    m_OpenDoor = true;
                    m_Sounds[0].Play();

                    if (m_EnemysBehindDoor)
                    {
                        m_Sounds[1].Play();
                    }

                    for (int i = 0; i < 2; i++)
                    {
                        m_DoorLights[i].GetComponent<Renderer>().material.color = Color.green;
                        m_DoorLights[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                        m_DoorLights[i].GetComponent<Light>().color = Color.green;
                    }
                }
            }
        }
    }
}

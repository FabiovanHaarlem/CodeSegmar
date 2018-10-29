using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObjectMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_RotatingMenuObjects;
    private GameObject m_CurrentObject;

    private float m_RotateTimer;
    private int m_Index;
	
	void Start ()
    {
        for (int i = 0; i < m_RotatingMenuObjects.Length; i++)
        {
            m_RotatingMenuObjects[i].SetActive(false);
        }

        m_Index = Random.Range(0, m_RotatingMenuObjects.Length);
        m_CurrentObject = m_RotatingMenuObjects[m_Index];
        m_CurrentObject.SetActive(true);
        m_RotateTimer = 5f;
	}
	
	
	void Update ()
    {
        m_CurrentObject.transform.Rotate(Vector3.up * Time.deltaTime * 10);
        m_RotateTimer -= Time.deltaTime;

        if (m_RotateTimer <= 0f)
        {
            m_CurrentObject.SetActive(false);

            if (m_Index == m_RotatingMenuObjects.Length - 1)
            {
                m_Index = 0;
            }
            else
            {
                m_Index++;
            }

            m_CurrentObject = m_RotatingMenuObjects[m_Index];
            m_CurrentObject.SetActive(true);
            m_RotateTimer = 5f;
        }
	}
}

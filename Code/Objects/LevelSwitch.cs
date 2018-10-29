using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitch : MonoBehaviour 
{
    [Header("Unloaded And Loaded Levels")]
    [SerializeField]
    [Tooltip("The name of the level that needs to be unloaded")]
    private string m_UnloadedLevel;
    [SerializeField]
    [Tooltip("The name of the level that needs to be loaded")]
    private string m_LoadedLevel;

    [Header("Level Switch Doors")]
    [SerializeField]
    private GameObject m_LeftLevel1Door;
    [SerializeField]
    private GameObject m_RightLevel1Door;
    [SerializeField]
    private GameObject m_LeftLevel2Door;
    [SerializeField]
    private GameObject m_RightLevel2Door;

    [Header("Level Switch Doors Closed Positions")]
    [SerializeField]
    private Transform m_LeftLevel1DoorClosedPosition;
    [SerializeField]
    private Transform m_RightLevel1DoorClosedPosition;
    [SerializeField]
    private Transform m_LeftLevel2DoorClosedPosition;
    [SerializeField]
    private Transform m_RightLevel2DoorClosedPosition;
    private Transform m_LeftLevel1DoorOpenPosition;
    private Transform m_RightLevel1DoorOpenPosition;
    private Transform m_LeftLevel2DoorOpenPosition;
    private Transform m_RightLevel2DoorOpenPosition;

    private float m_DoorsOpenTime1;
    private float m_CurrentDoorsOpenTime1;
    private float m_DoorsOpenTime2;
    private float m_CurrentDoorsOpenTime2;
    private float m_GetTransformsTimer;

    private bool m_CloseDoors1;
    private bool m_CloseDoors2;
    private bool m_GetTransforms;


    void Start()
    {
        m_LeftLevel1DoorOpenPosition = m_LeftLevel1Door.GetComponent<Transform>();
        m_RightLevel1DoorOpenPosition = m_RightLevel1Door.GetComponent<Transform>();
        m_LeftLevel2DoorOpenPosition = m_LeftLevel2Door.GetComponent<Transform>();
        m_RightLevel2DoorOpenPosition = m_RightLevel2Door.GetComponent<Transform>();

        m_DoorsOpenTime1 = 20f;
        m_CurrentDoorsOpenTime1 = 0;
        m_DoorsOpenTime2 = 20f;
        m_CurrentDoorsOpenTime2 = 0;
        m_GetTransformsTimer = 0;
        m_GetTransforms = true;
    }

    void Update()
    {
        m_GetTransformsTimer += Time.deltaTime;

        if (m_GetTransforms && m_GetTransformsTimer >= 5)
        {

            m_GetTransforms = false;
        }

        if (m_CloseDoors1)
        {
            if (m_CurrentDoorsOpenTime1 < m_DoorsOpenTime1)
            {
                m_CurrentDoorsOpenTime1 += Time.deltaTime;
            }
            m_LeftLevel1Door.transform.position = Vector3.Lerp(m_LeftLevel1DoorOpenPosition.position, m_LeftLevel1DoorClosedPosition.position, m_CurrentDoorsOpenTime1 / m_DoorsOpenTime1);
            m_RightLevel1Door.transform.position = Vector3.Lerp(m_RightLevel1DoorOpenPosition.position, m_RightLevel1DoorClosedPosition.position, m_CurrentDoorsOpenTime1 / m_DoorsOpenTime1);

        }

        if (m_CloseDoors2)
        {
            if (m_CurrentDoorsOpenTime2 < m_DoorsOpenTime2)
            {
                m_CurrentDoorsOpenTime2 += Time.deltaTime;
            }
            m_LeftLevel2Door.transform.position = Vector3.Lerp(m_LeftLevel2DoorOpenPosition.position, m_LeftLevel2DoorClosedPosition.position, m_CurrentDoorsOpenTime2 / m_DoorsOpenTime2);
            m_RightLevel2Door.transform.position = Vector3.Lerp(m_RightLevel2DoorOpenPosition.position, m_RightLevel2DoorClosedPosition.position, m_CurrentDoorsOpenTime2 / m_DoorsOpenTime2);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LoadScene());
            m_CloseDoors1 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(UnloadScene());
            m_CloseDoors2 = true;
        }
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene(m_LoadedLevel, LoadSceneMode.Additive);
    }

    IEnumerator UnloadScene()
    {
        yield return new WaitForEndOfFrame();
        SceneManager.UnloadSceneAsync(m_UnloadedLevel);
    }
}

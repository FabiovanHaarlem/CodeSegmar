using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTutorialTrigger : MonoBehaviour
{
    private AudioSource m_TutorialChargeStation;

    private bool m_AudioPlayed;

    private void Start()
    {
        m_TutorialChargeStation = GetComponent<AudioSource>();

        m_AudioPlayed = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (!m_AudioPlayed)
            {
                m_TutorialChargeStation.Play();
                m_AudioPlayed = true;
            }
        }
    }
}

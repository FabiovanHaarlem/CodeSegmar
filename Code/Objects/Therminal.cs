using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Therminal : MonoBehaviour 
{
    [SerializeField]
    private List<GameObject> m_DoorControleButtons;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<Renderer>().material.color = Color.green;
                GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
                GetComponent<Light>().color = Color.green;

                for (int i = 0; i < m_DoorControleButtons.Count; i++)
                {
                    m_DoorControleButtons[i].GetComponent<ConsoleDoorControle>().UnlockDoor();
                }            
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] m_IntroAudios;
    [SerializeField]
    private Text m_Subtitle;
    [SerializeField]
    private GameObject m_SubtitleBox;
    [SerializeField]
    private GameObject m_BlackField;
    [SerializeField]
    private GameObject m_SkyCollider;
	
	void Start ()
    {
        m_SubtitleBox.SetActive(true);
        m_SkyCollider.SetActive(false);
        m_BlackField.SetActive(true);
        StartCoroutine(PlayIntro());
        m_Subtitle.text = "...";
    }

    private IEnumerator PlayIntro()
    {
        yield return new WaitForSeconds(2);
        m_Subtitle.text = "Anderson: This is Anderson to C-209…. Do you copy?";
        m_IntroAudios[0].Play();
        yield return new WaitForSeconds(m_IntroAudios[0].clip.length);
        m_Subtitle.text = "C-209: Copy, There are some static interferences.";
        m_IntroAudios[1].Play();
        yield return new WaitForSeconds(m_IntroAudios[1].clip.length);
        m_Subtitle.text = "Anderson: That’s okey, you are deep underground, so you will probably loose connection with the MM.";
        m_IntroAudios[2].Play();
        yield return new WaitForSeconds(m_IntroAudios[2].clip.length);
        m_Subtitle.text = "C-209: What is the mission sir?";
        m_IntroAudios[3].Play();
        yield return new WaitForSeconds(m_IntroAudios[3].clip.length);
        m_Subtitle.text = "Anderson: Your first objective is to clear the path and find your way to the main hall. From there out, clear the whole facility from the Mutants.";
        m_IntroAudios[4].Play();
        yield return new WaitForSeconds(m_IntroAudios[4].clip.length);
        m_Subtitle.text = "C-209: Mutants sir?";
        m_IntroAudios[5].Play();
        yield return new WaitForSeconds(m_IntroAudios[5].clip.length);
        m_Subtitle.text = "Anderson: Yes mutants. This facility is part of the Navi secret operations. The Mutants you will see are failed experiments. The S.E.G.M.A.R program made by the Navi to create super soldiers for their army.";
        m_IntroAudios[6].Play();
        yield return new WaitForSeconds(m_IntroAudios[6].clip.length);
        m_Subtitle.text = "Anderson: At the main hall, the door to the left is the way to the laboratory. The door in the centre leads the way to the main operation room. On the right is the test room. And C-209 rescue anyone who is still down here. ";
        m_IntroAudios[7].Play();
        yield return new WaitForSeconds(m_IntroAudios[7].clip.length);
        m_SubtitleBox.SetActive(false);
        m_SkyCollider.SetActive(true);
        m_BlackField.SetActive(false);    
    }

    public void EndSubtitle()
    {
        m_SubtitleBox.SetActive(true);
        m_Subtitle.text = "Anderson: C-209? C-209, do you copy?  Dammit, This is Mission Operator Anderson, we have lost connection with C-209.";
    }

    public void StopAllAudio()
    {
        m_IntroAudios[6].Stop();
        m_IntroAudios[7].Stop();
    }
}

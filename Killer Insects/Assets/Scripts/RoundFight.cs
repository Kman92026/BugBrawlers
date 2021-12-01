using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundFight : MonoBehaviour
{
    public GameObject Round1Text;
    public GameObject Round2Text;
    public GameObject Round3Text;
    public GameObject FightText;
    public AudioSource MyPlayer;
    public AudioSource MusicPlayer;
    public AudioClip FightAudio;
    public AudioClip Round1Audio;
    public AudioClip Round2Audio;
    public AudioClip Round3Audio;
    public float pauseTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        Round1Text.gameObject.SetActive(false);
        Round2Text.gameObject.SetActive(false);
        Round3Text.gameObject.SetActive(false);
        FightText.gameObject.SetActive(false);
        StartCoroutine(RoundSet());
    }

    IEnumerator RoundSet()
    {
        yield return new WaitForSeconds(0.2f);
        if (SaveScript.Round == 1)
        {
            yield return new WaitForSeconds(0.4f);
            Round1Text.gameObject.SetActive(true);
            MyPlayer.clip = Round1Audio;
            MyPlayer.Play();
            yield return new WaitForSeconds(pauseTime);
            Round1Text.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            FightText.gameObject.SetActive(true);
            MyPlayer.clip = FightAudio;
            MyPlayer.Play();
            yield return new WaitForSeconds(pauseTime);
            FightText.gameObject.SetActive(false);
            MusicPlayer.Play();
            SaveScript.timeOut = false;
            this.gameObject.SetActive(false);
        }
        else if (SaveScript.Round == 2)
        {
            yield return new WaitForSeconds(0.4f);
            Round2Text.gameObject.SetActive(true);
            MyPlayer.clip = Round2Audio;
            MyPlayer.Play();
            yield return new WaitForSeconds(pauseTime);
            Round2Text.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            FightText.gameObject.SetActive(true);
            MyPlayer.clip = FightAudio;
            MyPlayer.Play();
            yield return new WaitForSeconds(pauseTime);
            FightText.gameObject.SetActive(false);
            MusicPlayer.Play();
            SaveScript.timeOut = false;
            this.gameObject.SetActive(false);
        }
        else if (SaveScript.Round == 3)
        {
            yield return new WaitForSeconds(0.4f);
            Round3Text.gameObject.SetActive(true);
            MyPlayer.clip = Round3Audio;
            MyPlayer.Play();
            yield return new WaitForSeconds(pauseTime);
            Round3Text.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            FightText.gameObject.SetActive(true);
            MyPlayer.clip = FightAudio;
            MyPlayer.Play();
            yield return new WaitForSeconds(pauseTime);
            FightText.gameObject.SetActive(false);
            MusicPlayer.Play();
            SaveScript.timeOut = false;
            this.gameObject.SetActive(false);
        }
    }

}

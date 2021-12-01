using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseScript : MonoBehaviour
{
    public GameObject WinText;
    public GameObject LoseText;
    public GameObject Player1WinText;
    public GameObject Player2WinText;
    public AudioSource MyPlayer;
    public AudioClip LoseAudio;
    public AudioClip Player1Audio;
    public AudioClip Player2Audio;
    public float pauseTime = 1.0f;
    private int Scene = 2;
 
    // Start is called before the first frame update
    void Start()
    {
        WinText.gameObject.SetActive(false);
        LoseText.gameObject.SetActive(false);
        Player1WinText.gameObject.SetActive(false);
        Player2WinText.gameObject.SetActive(false);
        StartCoroutine(WinSet());
    }

    IEnumerator WinSet()
    {
        if(SaveScript.Player1Health > SaveScript.Player2Health)
        {
            if (SaveScript.Player1Mode == true)
            {
                WinText.gameObject.SetActive(true);
                MyPlayer.Play();
                SaveScript.Player1Wins++;
            }
            else if(SaveScript.Player1Mode == false)
            {
                Player1WinText.gameObject.SetActive(true);
                MyPlayer.clip = Player1Audio;
                MyPlayer.Play();
                SaveScript.Player1Wins++;
            }
        }
        else if(SaveScript.Player1Health < SaveScript.Player2Health)
        {
            if (SaveScript.Player1Mode == true)
            {
                LoseText.gameObject.SetActive(true);
                MyPlayer.clip = LoseAudio;
                MyPlayer.Play();
                SaveScript.Player2Wins++;
            }
            else if (SaveScript.Player1Mode == false)
            {
                Player2WinText.gameObject.SetActive(true);
                MyPlayer.clip = Player2Audio;
                MyPlayer.Play();
                SaveScript.Player2Wins++;
            }
        }
        yield return new WaitForSeconds(pauseTime);
        SceneManager.LoadScene(Scene);
    }

}

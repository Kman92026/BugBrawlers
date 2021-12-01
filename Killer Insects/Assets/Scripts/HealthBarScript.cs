using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarScript : MonoBehaviour
{
    public Image Player1Green;
    public Image Player2Green;
    public Image Player1Red;
    public Image Player2Red;
    public Image Player1Win1;
    public Image Player1Win2;
    public Image Player2Win1;
    public Image Player2Win2;
    public TextMeshProUGUI TimerText;
    public GameObject WinCondition;
    public float levelTime = 90f;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        SaveScript.Round++;
        SaveScript.timeOut = true;
        if(SaveScript.Player1Wins == 1)
        {
            Player1Win1.gameObject.SetActive(true);
        }
        if (SaveScript.Player1Wins == 2)
        {
            Player1Win1.gameObject.SetActive(true);
            Player1Win2.gameObject.SetActive(true);
        }
        if (SaveScript.Player2Wins == 1)
        {
            Player2Win1.gameObject.SetActive(true);
        }
        if (SaveScript.Player2Wins == 2)
        {
            Player2Win1.gameObject.SetActive(true);
            Player2Win2.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TimerScript();
        HealthBarLogic();
    }

    private void TimerScript()
    {
        if (levelTime > 0 && SaveScript.timeOut == false)
        {
            levelTime -= 1 * Time.deltaTime;
        }
        else if (levelTime <= 0.1)
        {
            SaveScript.timeOut = true;
            WinCondition.gameObject.SetActive(true);
            WinCondition.gameObject.GetComponent<WinLoseScript>().enabled = true;

        }
        TimerText.text = Mathf.Round(levelTime).ToString();
    }

    private void HealthBarLogic()
    {
        Player1Green.fillAmount = SaveScript.Player1Health;
        Player2Green.fillAmount = SaveScript.Player2Health;

        if (SaveScript.Player2HealthTimer > 0)
        {
            SaveScript.Player2HealthTimer -= 2.0f * Time.deltaTime;
        }
        else if (SaveScript.Player2HealthTimer <= 0)
        {
            if (Player2Red.fillAmount > SaveScript.Player2Health)
            {
                Player2Red.fillAmount -= 0.003f;
            }
        }

        if (SaveScript.Player1HealthTimer > 0)
        {
            SaveScript.Player1HealthTimer -= 2.0f * Time.deltaTime;
        }
        else if (SaveScript.Player1HealthTimer <= 0)
        {
            if (Player1Red.fillAmount > SaveScript.Player1Health)
            {
                Player1Red.fillAmount -= 0.003f;
            }
        }
    }
}

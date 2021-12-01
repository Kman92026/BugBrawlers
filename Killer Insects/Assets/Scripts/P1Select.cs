using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class P1Select : MonoBehaviour
{
    public int MaxIcons = 3;
    public int IconsPerRow = 3;
    public int MaxRows = 1;

    public GameObject AntP1;
    public GameObject PopeP1;
    public GameObject RingP1;
    public GameObject AntP1Text;
    public GameObject PopeP1Text;
    public GameObject RingoP1Text;
    public TextMeshProUGUI Player1Name;
    public string CharacterSelectionP1 = "AntonyP1";

    private int IconNumber = 1;
    private int RowNumber = 1;
    private float PauseTime = 0.5f;
    private bool TimeCountDown = false;
    private bool ChangeCharacter = false;
    private AudioSource MyPlayer;


    // Start is called before the first frame update
    void Start()
    {
        SaveScript.Round = 0;
        SaveScript.Player1Wins = 0;
        SaveScript.Player2Wins = 0;
        Time.timeScale = 1.0f;
        ChangeCharacter = true;
        MyPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ChangeCharacter == true)
        {
            if (IconNumber == 1)
            {
                SwitchOff();
                AntP1.gameObject.SetActive(true);
                AntP1Text.gameObject.SetActive(true);
                Player1Name.text = "Antony";
                CharacterSelectionP1 = "AntonyP1";
                ChangeCharacter = false;
            }
            else if (IconNumber == 2)
            {
                SwitchOff();
                PopeP1.gameObject.SetActive(true);
                PopeP1Text.gameObject.SetActive(true);
                Player1Name.text = "Pope";
                CharacterSelectionP1 = "PopeP1";
                ChangeCharacter = false;
            }
            else if (IconNumber == 3)
            {
                SwitchOff();
                RingP1.gameObject.SetActive(true);
                RingoP1Text.gameObject.SetActive(true);
                Player1Name.text = "Ringo";
                CharacterSelectionP1 = "RingoP1";
                ChangeCharacter = false;
            }
        }

        if (TimeCountDown == true)
        {
            if(PauseTime > 0.1f)
            {
                PauseTime -= 0.5f * Time.deltaTime;
            }
            else if(PauseTime <= 0.1f)
            {
                PauseTime = 0.5f;
                TimeCountDown = false;
            }
        }

        if (Input.GetButtonDown("Submit"))
        {
            SaveScript.P1Select = CharacterSelectionP1;
            MyPlayer.Play();
            NextPlayer();
        }

        if(Input.GetAxis("Horizontal") > 0)
        {
            if(PauseTime == 0.5f)
            {
                if(IconNumber < IconsPerRow * RowNumber)
                {
                    IconNumber++;
                    ChangeCharacter = true;
                    TimeCountDown = true;
                }
            }
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            if (PauseTime == 0.5f)
            {
                if (IconNumber > IconsPerRow * (RowNumber -1) + 1)
                {
                    IconNumber = IconNumber - 1;
                    ChangeCharacter = true;
                    TimeCountDown = true;
                }
            }
        }

    }

    void SwitchOff()
    {
        AntP1.gameObject.SetActive(false);
        PopeP1.gameObject.SetActive(false);
        RingP1.gameObject.SetActive(false);
        AntP1Text.gameObject.SetActive(false);
        PopeP1Text.gameObject.SetActive(false);
        RingoP1Text.gameObject.SetActive(false);
    }

    void NextPlayer()
    {
        AntP1Text.gameObject.SetActive(false);
        PopeP1Text.gameObject.SetActive(false);
        RingoP1Text.gameObject.SetActive(false);
        if(SaveScript.Player1Mode == true)
        {
            this.GetComponent<CPUSelect>().enabled = true;
            this.GetComponent<P1Select>().enabled = false;
        }
        else if (SaveScript.Player1Mode == false)
        {
            this.GetComponent<P2Select>().enabled = true;
            this.GetComponent<P1Select>().enabled = false;
        }
    }
}

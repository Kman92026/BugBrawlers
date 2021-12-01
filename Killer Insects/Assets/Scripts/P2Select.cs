using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class P2Select : MonoBehaviour
{
    public int MaxIcons = 3;
    public int IconsPerRow = 3;
    public int MaxRows = 1;
    public int Scene = 1;

    public GameObject AntP2;
    public GameObject PopeP2;
    public GameObject RingP2;
    public GameObject AntP2Text;
    public GameObject PopeP2Text;
    public GameObject RingoP2Text;
    public TextMeshProUGUI Player2Name;
    public string CharacterSelectionP2 = "AntonyP2";

    private int IconNumber = 1;
    private int RowNumber = 1;
    private float PauseTime = 0.5f;
    private bool TimeCountDown = false;
    private bool ChangeCharacter = false;
    private AudioSource MyPlayer;


    // Start is called before the first frame update
    void Start()
    {
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
                AntP2.gameObject.SetActive(true);
                AntP2Text.gameObject.SetActive(true);
                Player2Name.text = "Antony";
                CharacterSelectionP2 = "AntonyP2";
                ChangeCharacter = false;
            }
            else if (IconNumber == 2)
            {
                SwitchOff();
                PopeP2.gameObject.SetActive(true);
                PopeP2Text.gameObject.SetActive(true);
                Player2Name.text = "Pope";
                CharacterSelectionP2 = "PopeP2";
                ChangeCharacter = false;
            }
            else if (IconNumber == 3)
            {
                SwitchOff();
                RingP2.gameObject.SetActive(true);
                RingoP2Text.gameObject.SetActive(true);
                Player2Name.text = "Ringo";
                CharacterSelectionP2 = "RingoP2";
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
            SaveScript.P2Select = CharacterSelectionP2;
            MyPlayer.Play();
            SceneManager.LoadScene(Scene);
        }

        if(Input.GetAxis("HorizontalP2") > 0)
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
        if (Input.GetAxis("HorizontalP2") < 0)
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
        AntP2.gameObject.SetActive(false);
        PopeP2.gameObject.SetActive(false);
        RingP2.gameObject.SetActive(false);
        AntP2Text.gameObject.SetActive(false);
        PopeP2Text.gameObject.SetActive(false);
        RingoP2Text.gameObject.SetActive(false);
    }
}

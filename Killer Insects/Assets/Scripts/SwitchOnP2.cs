using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchOnP2 : MonoBehaviour
{
    private bool victory = false;

    public GameObject P2Icon;
    public GameObject P2Character;
    public GameObject CPUCharacter;
    public string P2Name = "Player 2";
    public TextMeshProUGUI P2Text;
    public GameObject VictoryScreen;
    public float WaitTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        P2Icon.gameObject.SetActive(true);
        P2Text.text = P2Name;
        if (SaveScript.Player1Mode == false)
        {
            SaveScript.Player2Load = P2Character;
        }
        else
        {
            SaveScript.Player2Load = CPUCharacter;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (SaveScript.Player2Wins > 1)
        {
            if (victory == false)
            {
                victory = true;
                StartCoroutine(SetVictory());
            }
        }
        if (SaveScript.Player1Wins > 1)
        {
            if (victory == false)
            {
                victory = true;
                StartCoroutine(IconOff());
            }
        }
    }

    IEnumerator SetVictory()
    {
        
        yield return new WaitForSeconds(WaitTime);
        VictoryScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    IEnumerator IconOff()
    {
        yield return new WaitForSeconds(WaitTime);
        P2Icon.gameObject.SetActive(false);
    }

}

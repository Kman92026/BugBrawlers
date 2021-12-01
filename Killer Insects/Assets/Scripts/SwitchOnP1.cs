using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchOnP1 : MonoBehaviour
{
    private bool victory = false;

    public GameObject P1Icon;
    public GameObject P1Character;
    public string P1Name = "Player 1";
    public TextMeshProUGUI P1Text;
    public GameObject VictoryScreen;
    public float WaitTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        P1Icon.gameObject.SetActive(true);
        P1Text.text = P1Name;
        SaveScript.Player1Load = P1Character;

    }

    // Update is called once per frame
    void Update()
    {
        if(SaveScript.Player1Wins > 1)
        {
            if (victory == false)
            {
                victory = true;
                StartCoroutine(SetVictory());
            }
        }
        if(SaveScript.Player2Wins > 1)
        {
            if(victory == false)
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
        P1Icon.gameObject.SetActive(false);
    }
}

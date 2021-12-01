using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    public static float Player1Health = 1.0f;
    public static float Player2Health = 1.0f;
    public static float Player1HealthTimer = 2.0f;
    public static float Player2HealthTimer = 2.0f;
    public static bool timeOut = false;
    public static bool Player1Mode = true;
    public static int Player1Wins = 0;
    public static int Player2Wins = 0;
    public static int Round = 0;
    public static string P1Select;
    public static string P2Select;
    public static GameObject Player1Load;
    public static GameObject Player2Load;

    // Start is called before the first frame update
    void Start()
    {
        //P1Select = "PopeP1";
        //P2Select = "RingoP2";
        Player1Health = 1.0f;
        Player2Health = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Kyle Allen
 * Code based on course How to Make a Fighting Game by Pete Jepson
 * 
 * Description: This code will handle all the movement and all combat
 * actions of the player one character.
 */
public class Player1Movement : MonoBehaviour
{
    private Animator anim;
    private AnimatorStateInfo Player1AnimLayer; //gets info on the current layer of the animator controller
    private Vector3 oppPosition;
    private AudioSource PlayerSounds;
    private bool canWalkLeft = true;
    private bool canWalkRight = true;
    private bool isWalking = false;

    public static bool isJumpingPlayer1 = false;
    public static bool isFacingLeftP1 = false;
    public static bool isFacingRightP1 = true;
    public static bool walkLeftP1 = true;
    public static bool walkRightP1 = true;
    public float movementSpeed = 0.5f;
    public GameObject Player;
    public GameObject Opponent;
    public GameObject JumpActivator;
    public GameObject MoveDetector;
    public GameObject Restrict;
    public GameObject WinCondition;
    public AudioClip punch;
    public AudioClip kick;


    // Start is called before the first frame update
    void Start()
    {
        isJumpingPlayer1 = false;
        isFacingLeftP1 = false;
        isFacingRightP1 = true;
        walkLeftP1 = true;
        walkRightP1 = true;
        Opponent = GameObject.Find("Player2");
        WinCondition = GameObject.Find("WinCondition");
        WinCondition.gameObject.SetActive(false);
        anim = GetComponentInChildren<Animator>();
        anim.SetLayerWeight(1, 1);
        PlayerSounds = GetComponentInChildren<AudioSource>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveScript.timeOut == false)
        {
            Player1AnimLayer = anim.GetCurrentAnimatorStateInfo(0);

            FaceOpponent();
            HorizontalMovement();
            VerticalMovement();
            MovementRestrictions();
            CheckIfDead();
            CheckIfWon();
        }
        else
        {
            anim.SetBool("isWalking", false);
            oppPosition = Opponent.transform.position;

            if (oppPosition.x > Player.transform.position.x && !isFacingLeftP1)
            {
                StartCoroutine(LeftPostionFace());
            }
            else if (oppPosition.x < Player.transform.position.x && !isFacingRightP1)
            {
                StartCoroutine(RightPostionFace());
            }
        }
    }

    private void CheckIfWon()
    {
        if (SaveScript.Player2Health <= 0)
        {
            anim.SetTrigger("Victory");
            Player.GetComponent<Player1Actions>().enabled = false;
            StartCoroutine(StopMovement());
            WinCondition.SetActive(true);
            WinCondition.gameObject.GetComponent<WinLoseScript>().enabled = true;
        }
    }

    private void CheckIfDead()
    {
        //check if we are dead
        if (SaveScript.Player1Health <= 0)
        {
            anim.SetTrigger("Knockout");
            Player.GetComponent<Player1Actions>().enabled = false;
            StartCoroutine(StopMovement());
            WinCondition.SetActive(true);
            WinCondition.gameObject.GetComponent<WinLoseScript>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FistLight"))
        {
            anim.SetTrigger("LightReact");
            PlayerSounds.clip = punch;
            PlayerSounds.Play();
        }
        else if (other.gameObject.CompareTag("FistMedium"))
        {
            anim.SetTrigger("MediumReact");
            PlayerSounds.clip = punch;
            PlayerSounds.Play();
        }
        else if (other.gameObject.CompareTag("FistHeavy"))
        {
            anim.SetTrigger("HeavyReact");
            PlayerSounds.clip = punch;
            PlayerSounds.Play();
        }
        else if (other.gameObject.CompareTag("SuperAttack"))
        {
            anim.SetTrigger("SuperReact");
            PlayerSounds.clip = punch;
            PlayerSounds.Play();
        }
        else if (other.gameObject.CompareTag("KickLight"))
        {
            anim.SetTrigger("LightReact");
            PlayerSounds.clip = kick;
            PlayerSounds.Play();
        }
        else if (other.gameObject.CompareTag("KickMedium"))
        {
            anim.SetTrigger("MediumReact");
            PlayerSounds.clip = kick;
            PlayerSounds.Play();
        }
        else if (other.gameObject.CompareTag("KickHeavy"))
        {
            anim.SetTrigger("HeavyReact");
            PlayerSounds.clip = kick;
            PlayerSounds.Play();
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LeftBorder")
        {
            canWalkLeft = false;
        }
        else if (collision.gameObject.tag == "RightBorder")
        {
            canWalkRight = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        canWalkLeft = true;
        canWalkRight = true;
    }

    /*This function deals with all movement restrictions
     */
    private void MovementRestrictions()
    {
        //Stop player form landing on other player
        if (isJumpingPlayer1)
        {
            JumpActivator.SetActive(true);
        }
        else
        {
            JumpActivator.SetActive(false);
        }

        //stop player from moving other player
        if (isWalking)
        {
            MoveDetector.SetActive(true);
        }
        else
        {
            MoveDetector.SetActive(false);
        }

        if (MoveDetector.activeSelf == false)
        {
            walkLeftP1 = true;
            walkRightP1 = true;
        }
    }

    /* This Function Controls all vertical movement
     * actions of the player
     */
    private void VerticalMovement()
    {
        //Jumping & Crouching
        if (Input.GetAxis("Vertical") > 0)
        {
            if (isJumpingPlayer1 == false)
            {
                isJumpingPlayer1 = true;
                anim.SetTrigger("JumpTrigger");
                anim.SetBool("isWalking", false);
                StartCoroutine(JumpPause());
            }
        }
        else if (Input.GetAxis("Vertical") < 0 && !isJumpingPlayer1)
        {
            anim.SetBool("isCrouching", true);
        }
        else if (Input.GetAxis("Vertical") == 0)
        {
            anim.SetBool("isCrouching", false);
        }
    }

    /* This Function will handle all horizontal movement
     * actions for the player
     */
    private void HorizontalMovement()
    {
        if (Player1AnimLayer.IsTag("Motion"))
        {
            Time.timeScale = 1.0f;

            //Left & Right Walking
            if (Input.GetAxis("Horizontal") > 0 && !isJumpingPlayer1 && canWalkRight)
            {
                if (walkRightP1)
                {
                    anim.SetBool("isWalking", true);
                    transform.Translate(-movementSpeed * Time.deltaTime, 0, 0);
                    isWalking = true;
                }
            }
            else if (Input.GetAxis("Horizontal") < 0 && !isJumpingPlayer1 && canWalkLeft)
            {
                if (walkLeftP1)
                {
                    anim.SetBool("isWalking", true);
                    transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
                    isWalking = true;
                }
            }
            else if (Input.GetAxis("Horizontal") == 0 || isJumpingPlayer1)
            {
                anim.SetBool("isWalking", false);
                isWalking = false;
            }
            //Moving in the air
            else if (isJumpingPlayer1 && Input.GetAxis("Horizontal") > 0)
            {
                transform.Translate(-movementSpeed * Time.deltaTime, 0, 0);
            }
            else if (isJumpingPlayer1 && Input.GetAxis("Horizontal") < 0)
            {
                transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
            }
        }
    }

    /*This function controls everything needed to calculate
     * if the player needs to flip in order to face the 
     * opponent
     */
    private void FaceOpponent()
    {

        oppPosition = Opponent.transform.position;

        if (oppPosition.x > Player.transform.position.x && !isFacingLeftP1)
        {
            StartCoroutine(LeftPostionFace());
        }
        else if (oppPosition.x < Player.transform.position.x && !isFacingRightP1)
        {
            StartCoroutine(RightPostionFace());
        }
    }

    /* This function is used to set a wait time for jumping.
     * Should disable the double jump bug
     */
    IEnumerator JumpPause()
    {
        yield return new WaitForSeconds(2f);
        isJumpingPlayer1 = false;
    }

    /* The two function below are used to add a slight delay to
     * flipping the model for a smoother tranistion
     */
    IEnumerator LeftPostionFace()
    {
        isFacingLeftP1 = true;
        isFacingRightP1 = false;
        yield return new WaitForSeconds(0.45f);
        Player.transform.Rotate(0, -180, 0);
        anim.SetLayerWeight(1, 0);
    }

    IEnumerator RightPostionFace()
    {
        isFacingLeftP1 = false;
        isFacingRightP1 = true;
        yield return new WaitForSeconds(0.45f);
        Player.transform.Rotate(0, 180, 0);
        anim.SetLayerWeight(1, 1);
    }

    IEnumerator StopMovement()
    {
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<Player1Movement>().enabled = false;
    }
}

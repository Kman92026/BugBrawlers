using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Kyle Allen
 * Code based on course How to Make a Fighting Game by Pete Jepson
 * 
 * Description: This code will handle all the movement and all combat
 * actions of the player one character.
 */
public class Player2Movement : MonoBehaviour
{
    private Animator anim;
    private AnimatorStateInfo Player2AnimLayer; //gets info on the current layer of the animator controller
    private Vector3 oppPosition;
    private AudioSource PlayerSounds;
    private bool canWalkLeft = true;
    private bool canWalkRight = true;
    private bool isWalking = false;

    public static bool isJumpingPlayer2 = false;
    public static bool isFacingLeftP2 = false;
    public static bool isFacingRightP2 = true;
    public static bool walkLeftP2 = true;
    public static bool walkRightP2 = true;
    public float movementSpeed = 0.5f;
    public GameObject Player;
    public GameObject Opponent;
    public GameObject JumpActivator;
    public GameObject MoveDetector;
    public AudioClip punch;
    public AudioClip kick;


    // Start is called before the first frame update
    void Start()
    {
        isJumpingPlayer2 = false;
        isFacingLeftP2 = false;
        isFacingRightP2 = true;
        walkLeftP2 = true;
        walkRightP2 = true;
        Opponent = GameObject.Find("Player1");
        anim = GetComponentInChildren<Animator>();
        anim.SetLayerWeight(1, 0);
        PlayerSounds = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveScript.timeOut == false)
        {
            Player2AnimLayer = anim.GetCurrentAnimatorStateInfo(0);

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

            if (oppPosition.x > Player.transform.position.x && !isFacingLeftP2)
            {
                StartCoroutine(LeftPostionFace());
            }
            else if (oppPosition.x < Player.transform.position.x && !isFacingRightP2)
            {
                StartCoroutine(RightPostionFace());
            }
        }
    }

    private void CheckIfWon()
    {
        if (SaveScript.Player1Health <= 0)
        {
            anim.SetTrigger("Victory");
            Player.GetComponent<Player2Actions>().enabled = false;
            StartCoroutine(StopMovement());
        }
    }

    private void CheckIfDead()
    {
        //check if we are dead
        if (SaveScript.Player2Health <= 0)
        {
            anim.SetTrigger("Knockout");
            Player.GetComponent<Player2Actions>().enabled = false;
            StartCoroutine(StopMovement());
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
        //stop players form landing on each other
        if (isJumpingPlayer2)
        {
            JumpActivator.SetActive(true);
        }
        else
        {
            JumpActivator.SetActive(false);
        }

        //stop players from pushing each other
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
            walkLeftP2 = true;
            walkRightP2 = true;
        }
    }

    /* This Function Controls all vertical movement
     * actions of the player
     */
    private void VerticalMovement()
    {
        //Jumping & Crouching
        if (Input.GetAxis("VerticalP2") > 0)
        {
            if (isJumpingPlayer2 == false)
            {
                isJumpingPlayer2 = true;
                anim.SetTrigger("JumpTrigger");
                anim.SetBool("isWalking", false);
                StartCoroutine(JumpPause());
            }
        }
        else if (Input.GetAxis("VerticalP2") < 0 && !isJumpingPlayer2)
        {
            anim.SetBool("isCrouching", true);
        }
        else if (Input.GetAxis("VerticalP2") == 0)
        {
            anim.SetBool("isCrouching", false);
        }
    }

    /* This Function will handle all horizontal movement
     * actions for the player
     */
    private void HorizontalMovement()
    {
        if (Player2AnimLayer.IsTag("Motion"))
        {
            Time.timeScale = 1.0f;
            //Left & Right Walking
            if (Input.GetAxis("HorizontalP2") > 0 && !isJumpingPlayer2 && canWalkRight)
            {
                if (walkRightP2)
                {
                    anim.SetBool("isWalking", true);
                    transform.Translate(-movementSpeed * Time.deltaTime, 0, 0);
                    isWalking = true;
                }
            }
            else if (Input.GetAxis("HorizontalP2") < 0 && !isJumpingPlayer2 && canWalkLeft)
            {
                if (walkLeftP2 == true)
                {
                    anim.SetBool("isWalking", true);
                    transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
                    isWalking = true;
                }
            }
            else if (Input.GetAxis("HorizontalP2") == 0 || isJumpingPlayer2)
            {
                anim.SetBool("isWalking", false);
                isWalking = false;
            }
            //Moving in the air (Doesn't Work)
           /* else if (isJumpingPlayer2 && Input.GetAxis("HorizontalP2") > 0)
            {
                transform.Translate(-movementSpeed * Time.deltaTime, 0, 0);
            }
            else if (isJumpingPlayer2 && Input.GetAxis("HorizontalP2") < 0)
            {
                transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
            }*/
        }
    }

    /*This function controls everything needed to calculate
     * if the player needs to flip in order to face the 
     * opponent
     */
    private void FaceOpponent()
    {
        oppPosition = Opponent.transform.position;

        if (oppPosition.x > Player.transform.position.x && !isFacingLeftP2)
        {
            StartCoroutine(LeftPostionFace());
        }
        else if (oppPosition.x < Player.transform.position.x && !isFacingRightP2)
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
        isJumpingPlayer2 = false;
    }

    /* The two function below are used to add a slight delay to
     * flipping the model for a smoother tranistion
     */
    IEnumerator LeftPostionFace()
    {
        isFacingLeftP2 = true;
        isFacingRightP2 = false;
        yield return new WaitForSeconds(0.45f);
        Player.transform.Rotate(0, -180, 0);
        anim.SetLayerWeight(1, 0);
    }

    IEnumerator RightPostionFace()
    {
        isFacingLeftP2 = false;
        isFacingRightP2 = true;
        yield return new WaitForSeconds(0.45f);
        Player.transform.Rotate(0, 180, 0);
        anim.SetLayerWeight(1, 1);
    }

    IEnumerator StopMovement()
    {
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<Player2Movement>().enabled = false;
    }
}

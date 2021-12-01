using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Kyle Allen
 * Code based on course How to Make a Fighting Game by Pete Jepson
 * 
 * Description: This code will handle all the movement and all combat
 * actions of the player one character.
 */
public class Player2MovementCPU : MonoBehaviour
{
    private Animator anim;
    private AnimatorStateInfo Player2AnimLayer; //gets info on the current layer of the animator controller
    private Vector3 oppPosition;
    private AudioSource PlayerSounds;
    private bool canWalkLeft = true;
    private bool canWalkRight = true;
    private bool isWalking = false;
    private bool moveAI = true;
    public float oppDistance;
    private int crouch = 0;

    public static bool isJumpingCPU = false;
    public static bool isFacingLeftCPU = false;
    public static bool isFacingRightCPU = true;
    public static bool walkLeftCPU = true;
    public static bool walkRightCPU = true;
    public static bool attackState = false;
    public float movementSpeed = 0.5f;
    public float attackDistance = 1.5f;
    public GameObject Player;
    public GameObject Opponent;
    public GameObject JumpActivator;
    public GameObject MoveDetector;
    public AudioClip punch;
    public AudioClip kick;


    // Start is called before the first frame update
    void Start()
    {       
        isJumpingCPU = false;
        isFacingLeftCPU = false;
        isFacingRightCPU = true;
        walkLeftCPU = true;
        walkRightCPU = true;
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
            oppDistance = Vector3.Distance(Opponent.transform.position, Player.transform.position);

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

            if (oppPosition.x > Player.transform.position.x && !isFacingLeftCPU)
            {
                StartCoroutine(LeftPostionFace());
            }
            else if (oppPosition.x < Player.transform.position.x && !isFacingRightCPU)
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
            Player.GetComponent<Player2ActionsCPU>().enabled = false;
            StartCoroutine(StopMovement());
        }
    }

    private void CheckIfDead()
    {
        //check if we are dead
        if (SaveScript.Player2Health <= 0)
        {
            anim.SetTrigger("Knockout");
            Player.GetComponent<Player2ActionsCPU>().enabled = false;
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
            crouch = Random.Range(0, 5);
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
            crouch = Random.Range(0, 5);
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
        if (isJumpingCPU)
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
            walkLeftCPU = true;
            walkRightCPU = true;
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
            if (isJumpingCPU == false)
            {
                isJumpingCPU = true;
                anim.SetTrigger("JumpTrigger");
                anim.SetBool("isWalking", false);
                StartCoroutine(JumpPause());
            }
        }
        else if (crouch == 2 && !isJumpingCPU)
        {
            anim.SetBool("isCrouching", true);
            crouch = 0;
        }
       /* else if (Input.GetAxis("VerticalP2") == 0)
        {
            anim.SetBool("isCrouching", false);
        }*/
    }

    /* This Function will handle all horizontal movement
     * actions for the player
     */
    private void HorizontalMovement()
    {
        
        if (Player2AnimLayer.IsTag("Motion") || Player2AnimLayer.IsTag("Crouching"))
        {
            Time.timeScale = 1.0f;
            anim.SetBool("canAttack", false);
            //Left & Right Walking
            if (oppDistance > attackDistance && !isJumpingCPU && canWalkRight && isFacingLeftCPU)
            {
                if (moveAI)
                {
                    if (walkRightCPU)
                    {
                        anim.SetBool("isWalking", true);
                        attackState = false;
                        transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
                        isWalking = true;
                    }
                }
            }
            else if (oppDistance > attackDistance && !isJumpingCPU && canWalkLeft && isFacingRightCPU)
            {
                if (moveAI)
                {
                    if (walkLeftCPU == true)
                    {
                        anim.SetBool("isWalking", true);
                        attackState = false;
                        transform.Translate(-movementSpeed * Time.deltaTime, 0, 0);
                        isWalking = true;
                    }
                }
            }
            else if(oppDistance < attackDistance)
            {
                if (walkRightCPU || walkLeftCPU)
                {
                    if (moveAI)
                    {
                        moveAI = false;
                        anim.SetBool("isWalking", false);
                        anim.SetBool("canAttack", true);
                        StartCoroutine(ForwardPause());
                    }
                }
            }
           /* else if (Input.GetAxis("HorizontalP2") == 0 || isJumpingCPU)
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

        if (oppPosition.x > Player.transform.position.x && !isFacingLeftCPU)
        {
            StartCoroutine(LeftPostionFace());
        }
        else if (oppPosition.x < Player.transform.position.x && !isFacingRightCPU)
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
        isJumpingCPU = false;
    }

    /* The two function below are used to add a slight delay to
     * flipping the model for a smoother tranistion
     */
    IEnumerator LeftPostionFace()
    {
        isFacingLeftCPU = true;
        isFacingRightCPU = false;
        yield return new WaitForSeconds(0.45f);
        Player.transform.Rotate(0, -180, 0);
        anim.SetLayerWeight(1, 0);
    }

    IEnumerator RightPostionFace()
    {
        isFacingLeftCPU = false;
        isFacingRightCPU = true;
        yield return new WaitForSeconds(0.45f);
        Player.transform.Rotate(0, 180, 0);
        anim.SetLayerWeight(1, 1);
    }

    IEnumerator StopMovement()
    {
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<Player2MovementCPU>().enabled = false;
    }

    IEnumerator ForwardPause()
    {
        yield return new WaitForSeconds(0.4f);
        moveAI = true;
    }
}

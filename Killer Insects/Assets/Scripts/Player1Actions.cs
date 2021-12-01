using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Kyle Allen
 * Code based on course How to Make a Fighting Game by Pete Jepson
 * 
 * Description: This code will handle actions that need a more percise
 * sync with certain animations for smoother controlling. This code will 
 * also handle all attack action animations.
 */
public class Player1Actions : MonoBehaviour
{
    private Animator anim;
    private AnimatorStateInfo Player1AnimLayer;
    private AudioSource PlayerSounds;
    private bool superReact = false;

    public float jumpSpeed = 1.0f;
    public float superReactAmt = 4f;
    public GameObject Player;
    public AudioClip punchMiss;
    public AudioClip kickMiss;
    public static bool hitsP1 = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        PlayerSounds = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveScript.timeOut == false)
        {
            Player1AnimLayer = anim.GetCurrentAnimatorStateInfo(0);

            BasicAttacks();
            AerialAttacks();

            //Super React Slid
            if (superReact == true)
            {
                if (Player1Movement.isFacingRightP1 == true)
                {
                    Player.transform.Translate(superReactAmt * Time.deltaTime, 0, 0);
                }
                else if (Player1Movement.isFacingLeftP1 == true)
                {
                    Player.transform.Translate(-superReactAmt * Time.deltaTime, 0, 0);
                }
            }
        }
    }

    /*This Function handles all aerial combat
     */
    private void AerialAttacks()
    {
        if (Player1Movement.isJumpingPlayer1 == true)
        {
            if (Input.GetButtonDown("LPunch"))
            {
                anim.SetTrigger("LightPunchAir");
                hitsP1 = false;
            }
            else if (Input.GetButtonDown("LKick"))
            {
                anim.SetTrigger("LightKickAir");
                hitsP1 = false;
            }
        }
    }

    /* This function checks and excutes basic attacks based 
     * on the current state of the character
     */
    private void BasicAttacks()
    {
        //All basic attacks
        if (Player1Movement.isJumpingPlayer1 == false) //Stops players from using attack in air
        {
            if (Input.GetButtonDown("LPunch"))
            {
                anim.SetTrigger("LightPunch");
                hitsP1 = false;
            }
            else if (Input.GetButtonDown("MPunch"))
            {
                anim.SetTrigger("MediumPunch");
                hitsP1 = false;
            }
            else if (Input.GetButtonDown("HPunch"))
            {
                anim.SetTrigger("HeavyPunch");
                hitsP1 = false;
            }
            else if (Input.GetButtonDown("LKick"))
            {
                anim.SetTrigger("LightKick");
                hitsP1 = false;
            }
            else if (Input.GetButtonDown("MKick"))
            {
                anim.SetTrigger("MediumKick");
                hitsP1 = false;
            }
            else if (Input.GetButtonDown("HKick"))
            {
                anim.SetTrigger("HeavyKick");
                hitsP1 = false;
            }
        }
    }

    /* This function only is used when triggered by jump animation
     */
    public void JumpEvent()
    {
        if (Player1Movement.isFacingRightP1)
        {
            Player.transform.Translate(-4f, jumpSpeed * Time.deltaTime, 0);
        }
        else if (Player1Movement.isFacingLeftP1)
        {
            Player.transform.Translate(4f, jumpSpeed * Time.deltaTime, 0);
        }
    }

    public void PunchMissSound()
    {
        PlayerSounds.clip = punchMiss;
        PlayerSounds.Play();
    }

    public void KickMissSound()
    {
        PlayerSounds.clip = kickMiss;
        PlayerSounds.Play();
    }

    public void superReactSlide()
    {
        StartCoroutine(HitSlide());
    }

    public void RandomAttack()
    {
        //Used to stop that annoying ass error
    }

    IEnumerator HitSlide()
    {
        superReact = true;
        yield return new WaitForSeconds(0.3f);
        superReact = false;
    }
}

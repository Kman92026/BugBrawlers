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
public class Player2Actions : MonoBehaviour
{
    private Animator anim;
    private AnimatorStateInfo Player2AnimLayer;
    private AudioSource PlayerSounds;
    private bool superReact = false;

    public float jumpSpeed = 1.0f;
    public float superReactAmt = 4f;
    public GameObject Player;
    public AudioClip punchMiss;
    public AudioClip kickMiss;
    public static bool hitsP2 = false;

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
            Player2AnimLayer = anim.GetCurrentAnimatorStateInfo(0);

            BasicAttacks();
            AerialAttacks();

            //Super React Slid
            if (superReact == true)
            {
                if (Player2Movement.isFacingRightP2 == true)
                {
                    Player.transform.Translate(superReactAmt * Time.deltaTime, 0, 0);
                }
                else if (Player2Movement.isFacingLeftP2 == true)
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
        if (Player2Movement.isJumpingPlayer2 == true)
        {
            if (Input.GetButtonDown("LPunchP2"))
            {
                anim.SetTrigger("LightPunchAir");
                hitsP2 = false;
            }
            else if (Input.GetButtonDown("LKickP2"))
            {
                anim.SetTrigger("LightKickAir");
                hitsP2 = false;
            }
        }
    }

    /* This function checks and excutes basic attacks based 
     * on the current state of the character
     */
    private void BasicAttacks()
    {
        //All basic attacks
        if (Player2Movement.isJumpingPlayer2 == false) //Stops players from using attack in air
        {
            if (Input.GetButtonDown("LPunchP2"))
            {
                anim.SetTrigger("LightPunch");
                hitsP2 = false;
            }
            else if (Input.GetButtonDown("MPunchP2"))
            {
                anim.SetTrigger("MediumPunch");
                hitsP2 = false;
            }
            else if (Input.GetButtonDown("HPunchP2"))
            {
                anim.SetTrigger("HeavyPunch");
                hitsP2 = false;
            }
            else if (Input.GetButtonDown("LKickP2"))
            {
                anim.SetTrigger("LightKick");
                hitsP2 = false;
            }
            else if (Input.GetButtonDown("MKickP2"))
            {
                anim.SetTrigger("MediumKick");
                hitsP2 = false;
            }
            else if (Input.GetButtonDown("HKickP2"))
            {
                anim.SetTrigger("HeavyKick");
                hitsP2 = false;
            }
        }
    }

    /* This function only is used when triggered by jump animation
     */
    public void JumpEvent()
    {
        Player.transform.Translate(0.1f, jumpSpeed * Time.deltaTime, 0);
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

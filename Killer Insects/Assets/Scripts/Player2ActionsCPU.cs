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
public class Player2ActionsCPU : MonoBehaviour
{
    private Animator anim;
    private AnimatorStateInfo Player2AnimLayer;
    private AudioSource PlayerSounds;
    private bool superReact = false;
    private bool attacking = true;
    private int attackNumber = 1;

    public float jumpSpeed = 1.0f;
    public float superReactAmt = 4f;
    public float attackRate = 1.0f;
    public GameObject Player;
    public AudioClip punchMiss;
    public AudioClip kickMiss;
    public static bool hitsCPU = false;

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
                if (Player2MovementCPU.isFacingRightCPU == true)
                {
                    Player.transform.Translate(superReactAmt * Time.deltaTime, 0, 0);
                }
                else if (Player2MovementCPU.isFacingLeftCPU == true)
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
        if (Player2MovementCPU.isJumpingCPU == true)
        {
            if (Input.GetButtonDown("LPunchP2"))
            {
                anim.SetTrigger("LightPunchAir");
                hitsCPU = false;
            }
            else if (Input.GetButtonDown("LKickP2"))
            {
                anim.SetTrigger("LightKickAir");
                hitsCPU = false;
            }
        }
    }

    /* This function checks and excutes basic attacks based 
     * on the current state of the character
     */
    private void BasicAttacks()
    {
        //All basic attacks
        if (Player2MovementCPU.isJumpingCPU == false) //Stops players from using attack in air
        {
            if (Player2AnimLayer.IsTag("Crouching"))
            {
                StartCoroutine(AttackPause());
            }

            if (attacking == true)
            {
                attacking = false;
                if (attackNumber == 1)
                {
                    anim.SetTrigger("LightPunch");
                    hitsCPU = false;
                    StartCoroutine(AttackPause());
                    if (Player2AnimLayer.IsTag("Crouching"))
                    {
                        anim.SetBool("isCrouching", false);
                    }
                }
                else if (attackNumber == 2)
                {
                    anim.SetTrigger("MediumPunch");
                    hitsCPU = false;
                    StartCoroutine(AttackPause());
                    if (Player2AnimLayer.IsTag("Crouching"))
                    {
                        anim.SetBool("isCrouching", false);
                    }
                }
                else if (attackNumber == 3)
                {
                    anim.SetTrigger("HeavyPunch");
                    hitsCPU = false;
                    StartCoroutine(AttackPause());
                    if (Player2AnimLayer.IsTag("Crouching"))
                    {
                        anim.SetBool("isCrouching", false);
                    }
                }
                else if (attackNumber == 4)
                {
                    anim.SetTrigger("LightKick");
                    hitsCPU = false;
                    StartCoroutine(AttackPause());
                    if (Player2AnimLayer.IsTag("Crouching"))
                    {
                        anim.SetBool("isCrouching", false);
                    }
                }
                else if (attackNumber == 5)
                {
                    anim.SetTrigger("MediumKick");
                    hitsCPU = false;
                    StartCoroutine(AttackPause());
                    if (Player2AnimLayer.IsTag("Crouching"))
                    {
                        anim.SetBool("isCrouching", false);
                    }
                }
                else if (attackNumber == 6)
                {
                    anim.SetTrigger("HeavyKick");
                    hitsCPU = false;
                    StartCoroutine(AttackPause());
                    if (Player2AnimLayer.IsTag("Crouching"))
                    {
                        anim.SetBool("isCrouching", false);
                    }
                }
            }
        }
    }

    public void RandomAttack()
    {
        attackNumber = Random.Range(1, 7);
        //artCoroutine(AttackPause());
    }

    /* This function only is used when triggered by jump animation
     */
    public void JumpEvent()
    {
        if (Player2MovementCPU.isFacingRightCPU)
        {
            Player.transform.Translate(-4f, jumpSpeed * Time.deltaTime, 0);
        }
        else if (Player2MovementCPU.isFacingLeftCPU)
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
    IEnumerator HitSlide()
    {
        superReact = true;
        yield return new WaitForSeconds(0.3f);
        superReact = false;
    }

    IEnumerator AttackPause()
    {
        yield return new WaitForSeconds(attackRate);
        attacking = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2HitTrigger : MonoBehaviour
{
    private GameObject ChosenParticles;
    private ParticleSystem particles;

    public Collider col;
    public bool emitFX = false;
    public float DamageAmt = 0.01f;
    public float pauseSpeed = 0.01f;
    public string ParticleType = "Player1Particle1";

    private void Start()
    {
        ChosenParticles = GameObject.Find(ParticleType);
        particles = ChosenParticles.gameObject.GetComponent<ParticleSystem>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Player2Actions.hitsP2 == false)
        {
            col.enabled = true;
        }
        else
        {
            col.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            if (emitFX == true)
            {
                particles.Play();
                Time.timeScale = pauseSpeed;
            }
            Player2Actions.hitsP2 = true;
            SaveScript.Player1Health -= DamageAmt;
            if (SaveScript.Player1HealthTimer < 2.0f)
            {
                SaveScript.Player1HealthTimer += 2.0f;
            }
        }
    }
}

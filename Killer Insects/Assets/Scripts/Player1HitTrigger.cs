using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1HitTrigger : MonoBehaviour
{
    private GameObject ChosenParticles;
    private ParticleSystem particles;

    public Collider col;
    public bool emitFX = false;
    public float DamageAmt = 0.1f;
    public float pauseSpeed = 0.1f;
    public string ParticleType = "Player2Particle1";

    private void Start()
    {
        ChosenParticles = GameObject.Find(ParticleType);
        particles = ChosenParticles.gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player1Actions.hitsP1 == false)
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
        if (other.gameObject.CompareTag("Player2"))
        {
            if(emitFX == true)
            {
                particles.Play();
                Time.timeScale = pauseSpeed;
            }
            Player1Actions.hitsP1 = true;
            SaveScript.Player2Health -= DamageAmt;
            if (SaveScript.Player2HealthTimer < 2.0f)
            {
                SaveScript.Player2HealthTimer += 2.0f;
            }
        }
    }
}

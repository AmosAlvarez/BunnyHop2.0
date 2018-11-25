using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

    public Transform currentBlockCheck;

    ParticleSystem partGroundBrown;
    ParticleSystem partGroundGreen;
    ParticleSystem partGroundIce;
    ParticleSystem partGroundSnow;
    ParticleSystem partExitSnow;
    ParticleSystem partRock;

    public AudioClip groundHit;
    public AudioClip rockHit;
    public AudioClip jump;
    public AudioClip victory;
    public AudioClip defeat;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        currentBlockCheck = transform.Find("CurrentBlockCheck");

        partGroundBrown = transform.Find("Particles").transform.Find("GroundBrown").GetComponent<ParticleSystem>();
        partGroundGreen = transform.Find("Particles").transform.Find("GroundGreen").GetComponent<ParticleSystem>();
        partGroundIce = transform.Find("Particles").transform.Find("GroundIce").GetComponent<ParticleSystem>();
        partGroundSnow = transform.Find("Particles").transform.Find("GroundSnow").GetComponent<ParticleSystem>();
        partExitSnow = transform.Find("Particles").transform.Find("ExitSnow").GetComponent<ParticleSystem>();
        partRock = transform.Find("Particles").transform.Find("Rock").GetComponent<ParticleSystem>();

    }

    private void Update()
    {
        
    }

    void LandParticles()
    {
        Collider[] currentBlock = Physics.OverlapSphere(currentBlockCheck.position, 0.1f);

        if (currentBlock[0].gameObject.layer == 9)
        {
            partGroundBrown.Play();
        }
        else if (currentBlock[0].gameObject.layer == 10)
        {
            partGroundGreen.Play();
        }
        else if (currentBlock[0].gameObject.layer == 11)
        {
            partGroundIce.Play();
        }
        else if (currentBlock[0].gameObject.layer == 12)
        {
            partGroundSnow.Play();
        }
    }

    void ExitSnow()
    {
        partExitSnow.Play();
    }

    void BumpRock()
    {
        partRock.Play();
    }

    void SonidoSalto()
    {
        audioSource.clip = jump;
        audioSource.Play();
    }

    void SonidoMuerte()
    {
        audioSource.clip = defeat;
        audioSource.Play();
    }

    void SonidoGolpeRoca()
    {
        audioSource.clip = rockHit;
        audioSource.Play();
    }

    void SonidoAterriza()
    {
        audioSource.clip = groundHit;
        audioSource.Play();
    }
}

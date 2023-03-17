using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    //PARAMETERS - For tuning, typically set in the editor
    //CACHE - e.g. references for readability or speed
    //STATE - private instance (member) variables

    [SerializeField] float mainThrust = 1f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
            StartThrusting();
        else
            StopThrusting();

    }

     void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
            ApplyLeftThrust();
        else if (Input.GetKey(KeyCode.D))
            ApplyRightThrust();
        else
            StopRotationThrust();
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
        if (!mainThrustParticles.isPlaying)
            mainThrustParticles.Play();
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(mainEngine);
    }

    private void StopThrusting()
    {
        mainThrustParticles.Stop();
        audioSource.Stop();
    }


    private void ApplyLeftThrust()
    {
        if (!leftThrustParticles.isPlaying)
            leftThrustParticles.Play();
        ApplyRotation(rotationThrust);
    }

    private void ApplyRightThrust()
    {
        if (!rightThrustParticles.isPlaying)
            rightThrustParticles.Play();
        ApplyRotation(-rotationThrust);
    }

    private void StopRotationThrust()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}

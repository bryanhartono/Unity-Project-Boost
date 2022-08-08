using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrust_power = 1000f;
    [SerializeField] float rotation_power = 100f;

    [SerializeField] AudioClip main_engine;

    [SerializeField] ParticleSystem main_engine_particles;
    [SerializeField] ParticleSystem left_thruster_particles;
    [SerializeField] ParticleSystem right_thruster_particles;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {   
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotate();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrust_power * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(main_engine);
        }

        if (!main_engine_particles.isPlaying)
        {
            main_engine_particles.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        main_engine_particles.Stop();
    }

    void RotateLeft()
    {
        ApplyRotation(rotation_power);
        if (!right_thruster_particles.isPlaying)
        {
            right_thruster_particles.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-rotation_power);
        if (!left_thruster_particles.isPlaying)
        {
            left_thruster_particles.Play();
        }
    }

    void StopRotate()
    {
        right_thruster_particles.Stop();
        left_thruster_particles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}

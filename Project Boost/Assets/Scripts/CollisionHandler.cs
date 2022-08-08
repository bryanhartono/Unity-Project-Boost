using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // for scene manager

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float load_delay = 2f;

    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem success_particles;
    [SerializeField] ParticleSystem crash_particles;

    AudioSource audioSource;

    bool is_transitioning = false;
    bool collision_disabled = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();    
    }

    void Update()
    {
        RespondToDebugKeys(); // Disable before publishing.
        RestartLevel(); // To restart game
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collision_disabled = !collision_disabled; // toggle collision
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (is_transitioning || collision_disabled)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly.");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {   
        is_transitioning = true;
        audioSource.Stop(); // stops all sound
        audioSource.PlayOneShot(success);
        success_particles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", load_delay);
    }

    void StartCrashSequence()
    {
        is_transitioning = true;
        audioSource.Stop(); // stops all sound
        audioSource.PlayOneShot(crash);
        crash_particles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", load_delay);
    }

    void ReloadLevel()
    {   
        int current_scene_idx = SceneManager.GetActiveScene().buildIndex; // gets current scene index
        SceneManager.LoadScene(current_scene_idx);
    }

    void LoadNextLevel()
    {
        int current_scene_idx = SceneManager.GetActiveScene().buildIndex; // gets current scene index
        int next_scene_idx = current_scene_idx + 1;

        SceneManager.LoadScene(next_scene_idx);
    }

    void RestartLevel()
    {
        int current_scene_idx = SceneManager.GetActiveScene().buildIndex + 1; // gets current scene index + 1
        if (current_scene_idx == SceneManager.sceneCountInBuildSettings)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        } 
    }
}

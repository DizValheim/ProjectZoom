using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip successfulLanding;
    
    AudioSource audioSource;

    bool isTransitioning = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();    
    }

    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning) { return; }
        
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This object is friendly.");
                break;
            case "Fuel":
                Debug.Log("Fuel++");
                break;
            case "Finish":
                StartSuccessSequence();
                Debug.Log("Level Complete!");
                break;
            default:
                StartCrashSequence();
                break;
        }

    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        //todo add particle effects upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successfulLanding);
        //todo add particle effects upon success
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void LoadNextLevel()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}

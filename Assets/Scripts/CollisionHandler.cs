using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 1f;
   
    void OnCollisionEnter(Collision other) 
    {
        
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
        //todo add crash SFX
        //todo add particle effects upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartSuccessSequence()
    {
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

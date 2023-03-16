using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
   
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
                LoadNextLevel();
                Debug.Log("Level Complete!");
                break;
            default:
                ReloadLevel();
                Debug.Log("Whatever!");
                break;
        }

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

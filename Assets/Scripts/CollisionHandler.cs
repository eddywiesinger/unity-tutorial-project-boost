using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] float levelLoadDelay = 2f;
    bool isTransitioning = false;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip crashSound;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning) return;

        switch (other.gameObject.tag) {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Fuel":
                Debug.Log("Du hast Tank aufgesammelt");
                break;
            case "Finish":
                StartNextLevelSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }


    void ReloadLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);        
    }
    
    void LoadNextLevel() {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        
        SceneManager.LoadScene(nextSceneIndex);
    }

    void StartCrashSequence() {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        // TODO add particle effect
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartNextLevelSequence() {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        // TODO add particle effect
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }
}

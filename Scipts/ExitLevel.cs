using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    [SerializeField] float loadSceneDelay = 1f;
    [SerializeField] ParticleSystem exitParticle;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            exitParticle.Play();
            StartCoroutine(LoadNextScene()); 
        }
    }
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSecondsRealtime(loadSceneDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        FindObjectOfType<GamePersist>().ResetGamePersist();
        SceneManager.LoadScene(nextSceneIndex);
        // Debug.Log(SceneManager.sceneCountInBuildSettings);  2
    }
}

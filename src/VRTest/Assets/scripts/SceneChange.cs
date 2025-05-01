using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public float delaySeconds;       // Set delay time in seconds
    public string sceneToLoad;       // Set the name of your next scene

    private bool hasTriggered = false; // Prevent multiple triggers

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Hit: " + other.gameObject.name);

        if (!hasTriggered && other.gameObject.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(ChangeSceneAfterDelay());
        }
    }

    private System.Collections.IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene(sceneToLoad);
    }
}

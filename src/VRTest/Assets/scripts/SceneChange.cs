using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public float delaySeconds;       // Set delay time in seconds
    public string[] Scenes;
    [SerializeField] private Collectables collectables;
    private bool hasTriggered = false; // Prevent multiple triggers

    public GameObject[] panelsGropupone;
    public GameObject[] panelsGroptwo;
    public GameObject[] panelsGroupthree;


    private bool unlockone;
    private bool unlocktwo;
    private bool unlockthree;

    private void Start()
    {
        // Find the Collectables component in the scene
        collectables = FindObjectOfType<Collectables>();

        // Check if we found it, if not, log a warning
        if (collectables == null)
        {
            Debug.LogWarning("Collectables script not found in the scene!");
        }
        Debug.Log("Artifact one: " + collectables.getArtifactone());
        unlockone = collectables.getArtifactone();

        Debug.Log("Artifact two: " + collectables.getArtifacttwo());
        unlocktwo = collectables.getArtifacttwo();

        Debug.Log("Artifact three: " + collectables.getArtifactthree());
        unlockthree = collectables.getArtifactthree();
    }
    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Hit: " + other.gameObject.name);

        if (!hasTriggered && other.gameObject.CompareTag("Player"))
        {
            hasTriggered = true;

            if(!unlockone &&  !unlocktwo && !unlockthree)
            {
                StartCoroutine(ChangeSceneAfterDelay(Scenes[0]));
            }
            else if (unlockone && !unlocktwo && !unlockthree)
            {
                StartCoroutine(ChangeSceneAfterDelay(Scenes[1]));
            }
            else if (unlockone && unlocktwo && !unlockthree)
            {
                StartCoroutine(ChangeSceneAfterDelay(Scenes[2]));
            }
            else if (unlockone && unlocktwo && unlockthree)
            {
                StartCoroutine(ChangeSceneAfterDelay(Scenes[3]));
            }

        }
    }

    private System.Collections.IEnumerator ChangeSceneAfterDelay(string Scene)
    {
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene(Scene);
    }
    private void Update()
    {
        
        if (collectables != null)
        {


            if (unlockone)
            {
                for (int i = 0; i < panelsGropupone.Length; i++)
                {
                    Transform child = panelsGropupone[i].transform.Find("LockPannel");
                    if (child != null)
                    {
                        GameObject childObject = child.gameObject;
                        childObject.SetActive(false);
                    }
                }
            }

            if (unlocktwo)
            {
                for (int i = 0; i < panelsGroptwo.Length; i++)
                {
                    Transform child = panelsGroptwo[i].transform.Find("LockPannel");
                    if (child != null)
                    {
                        GameObject childObject = child.gameObject;
                        childObject.SetActive(false);
                    }
                }
            }

            if (unlockthree)
            {
                for (int i = 0; i < panelsGroupthree.Length; i++)
                {
                    Transform child = panelsGroupthree[i].transform.Find("LockPannel");
                    if (child != null)
                    {
                        GameObject childObject = child.gameObject;
                        childObject.SetActive(false);
                    }
                }
            }
        }
    }

}

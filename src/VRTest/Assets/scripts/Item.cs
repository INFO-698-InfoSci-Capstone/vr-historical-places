using UnityEngine;
using UnityEngine.SceneManagement;

public class Item : MonoBehaviour
{
    [Header("Bob Settings")]
    public float bobHeight = 0.5f;
    public float bobSpeed = 2f;

    [Header("Swirl Settings")]
    public float rotationSpeed = 90f; // degrees per second

    [Header("Scene Reference")]
    public string Scenecount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 startPosition;
    private bool hasTriggered = false;
    public float delaySeconds;       
    public string sceneToLoad;


    [SerializeField] private Collectables collectables;


    [Header("Sound")]
    public AudioClip pickupSound; // Optional override
    void Start()
    {
        startPosition = transform.position;
        // Find the Collectables component in the scene
        collectables = FindObjectOfType<Collectables>();

        // Check if we found it, if not, log a warning
        if (collectables == null)
        {
            Debug.LogWarning("Collectables script not found in the scene!");
        }
    }

    void Update()
    {
        // Bobbing (vertical movement using sine wave)
        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;

        // Apply bobbing
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        // Swirling (rotating around Y axis)
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log("Hit: " + other.gameObject.name);
        //Debug.Log("Hit: " + other.gameObject.ToString());
        if (!hasTriggered && other.gameObject.CompareTag("Player"))
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                if (pickupSound != null)
                    audioSource.PlayOneShot(pickupSound);
                else
                    audioSource.Play(); // fallback to assigned clip
            }

            if (Scenecount == "one")
            {
                collectables.setCollectArtifactOne(true);
                Debug.Log("Artifact one set: " + collectables.getArtifactone());
               
            }
            else if (Scenecount == "three")
            {
                collectables.setCollectArtifactThree(true);
                Debug.Log("Artifact three set: " + collectables.getArtifactthree());
            }
            hasTriggered = true;
            StartCoroutine(ChangeSceneAfterDelay());
            Destroy(gameObject, 0.5f); // Adjust to match clip length

        }
    }
    private System.Collections.IEnumerator ChangeSceneAfterDelay()
    {
        Debug.Log("Test"+ sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
        //yield return new WaitForSeconds(delaySeconds);
        yield return null;
        //return null;
    }
}

using UnityEngine;

public class ArtifactCollection : MonoBehaviour
{
    [Header("Bob Settings")]
    public float bobHeight = 0.5f;
    public float bobSpeed = 2f;

    [Header("Swirl Settings")]
    public float rotationSpeed = 90f; // degrees per second
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 startPosition;

    public AudioClip pickupSound; // Optional override
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
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
        if (other.CompareTag("Player"))
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                if (pickupSound != null)
                    audioSource.PlayOneShot(pickupSound);
                else
                    audioSource.Play(); // fallback to assigned clip
            }

            FindObjectOfType<ArtifactSpawnner>().ArtifactCollected();

            // Delay destroy so sound can finish
            Destroy(gameObject, 0.5f); // Adjust to match clip length
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class ArtifactSpawnner : MonoBehaviour
{
    public GameObject[] meshObjects;            // Mesh surfaces to spawn on
    public GameObject[] artifactPrefabs;        // 3 unique artifact prefabs (order matters)

    private int collectedCount = 0;
    public float delaySeconds;
    public string sceneToLoad;
    [SerializeField] private Collectables collectables;
    void Start()
    {
        collectables = FindObjectOfType<Collectables>();
        SpawnNextArtifact();
    }

    public void ArtifactCollected()
    {
        collectedCount++;

        if (collectedCount >= artifactPrefabs.Length)
        {
            Debug.Log("All artifacts collected! Loading next scene...");
            collectables.setCollectArtifactTwo(true);
            StartCoroutine(ChangeSceneAfterDelay());
        }
        else
        {
            SpawnNextArtifact();
        }
    }

    void SpawnNextArtifact()
    {
        if (artifactPrefabs.Length == 0 || collectedCount >= artifactPrefabs.Length) return;

        GameObject selectedMesh = meshObjects[Random.Range(0, meshObjects.Length)];
        Mesh mesh = selectedMesh.GetComponent<MeshFilter>().sharedMesh;
        Transform meshTransform = selectedMesh.transform;

        Vector3 spawnPos = GetRandomPointOnMesh(mesh, meshTransform);
        spawnPos.y = 2f;
        Debug.Log(spawnPos);
        Instantiate(artifactPrefabs[collectedCount], spawnPos, Quaternion.identity);
    }

    private System.Collections.IEnumerator ChangeSceneAfterDelay()
    {
        Debug.Log("Test" + sceneToLoad);
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene(sceneToLoad);
        //return null;
    }
    Vector3 GetRandomPointOnMesh(Mesh mesh, Transform transform)
    {
        Vector3[] verts = mesh.vertices;
        int[] tris = mesh.triangles;

        int triIndex = Random.Range(0, tris.Length / 3) * 3;

        Vector3 v0 = transform.TransformPoint(verts[tris[triIndex]]);
        Vector3 v1 = transform.TransformPoint(verts[tris[triIndex + 1]]);
        Vector3 v2 = transform.TransformPoint(verts[tris[triIndex + 2]]);

        float a = Random.value;
        float b = Random.value;

        if (a + b > 1f)
        {
            a = 1f - a;
            b = 1f - b;
        }

        return v0 + a * (v1 - v0) + b * (v2 - v0);
    }
}

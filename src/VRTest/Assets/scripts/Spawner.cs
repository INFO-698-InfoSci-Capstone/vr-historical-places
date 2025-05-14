using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject[] meshObjects; // Assign GameObjects with MeshColliders in Inspector

    void Start()
    {
        SpawnObject(objectToSpawn);
    }



    public void SpawnObject(GameObject objectToSpawn)
    {
        // Pick a random GameObject from the array
        GameObject selectedObject = meshObjects[Random.Range(0, meshObjects.Length)];

        MeshCollider meshCollider = selectedObject.GetComponent<MeshCollider>();
        if (meshCollider == null || meshCollider.sharedMesh == null)
        {
            Debug.LogWarning("Selected object has no MeshCollider or sharedMesh.");
            return;
        }

        Mesh mesh = meshCollider.sharedMesh;
        Transform meshTransform = meshCollider.transform;

        Vector3 randomPoint = GetRandomPointOnMesh(mesh, meshTransform);
        Debug.Log(randomPoint);
        randomPoint.y = 2f;
        Debug.Log(randomPoint);
        Instantiate(objectToSpawn, randomPoint, Quaternion.identity);
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

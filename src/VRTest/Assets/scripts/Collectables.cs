using UnityEngine;

public class Collectables : MonoBehaviour
{
    public static Collectables Instance;
    private bool artifactoneiscollecter;
    private bool artifacttwoiscollecter;
    private bool artifactthreeiscollecter;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate from the new scene
        }
    }

    public bool getArtifactone() { return artifactoneiscollecter; }
    public bool getArtifacttwo() { return artifacttwoiscollecter; }
    public bool getArtifactthree() { return artifactthreeiscollecter; }

    public void setCollectArtifactOne(bool value)
    {
        artifactoneiscollecter=value;
    }
    public void setCollectArtifactTwo(bool value)
    {
        artifacttwoiscollecter = value;
    }
    public void setCollectArtifactThree(bool value)
    {
        artifactthreeiscollecter = value;
    }
}

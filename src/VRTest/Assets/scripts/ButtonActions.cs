using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class ButtonActions : MonoBehaviour
{
    [SerializeField] private Collectables collectables;
    //private bool unlockone;
    //private bool unlocktwo;
    //private bool unlockthree;
    public void ActivePanel(GameObject panel)
    {
        Debug.Log(panel.gameObject.name);
        Debug.Log(panel.gameObject);
        panel.SetActive(true);
    }
    public void DeactivatePanel()
    {
        if (transform != null )
        {
            transform.gameObject.SetActive(false);
        }
    }


    public void StartGame(string Scene)
    {

        collectables = FindObjectOfType<Collectables>();

        // Check if we found it, if not, log a warning
        if (collectables == null)
        {
            Debug.LogWarning("Collectables script not found in the scene!");
        }
        //Debug.Log("Artifact one: " + collectables.getArtifactone());
        //unlockone = collectables.getArtifactone();

        //Debug.Log("Artifact two: " + collectables.getArtifacttwo());
        //unlocktwo = collectables.getArtifacttwo();

        //Debug.Log("Artifact three: " + collectables.getArtifactthree());
        //unlockthree = collectables.getArtifactthree();

        collectables.setCollectArtifactOne(false);
        collectables.setCollectArtifactTwo(false);
        collectables.setCollectArtifactThree(false);
        SceneManager.LoadScene(Scene);
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}

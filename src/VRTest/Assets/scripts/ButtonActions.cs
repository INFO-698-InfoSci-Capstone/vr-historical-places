using UnityEngine;

public class ButtonActions : MonoBehaviour
{

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

}

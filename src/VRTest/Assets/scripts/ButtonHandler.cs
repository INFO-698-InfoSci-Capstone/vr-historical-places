using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameManager gameManager;

    public void OnClick(int buttonIndex)
    {
        gameManager.OnButtonPressed(buttonIndex);
    }
}

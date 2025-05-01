using UnityEngine;
using UnityEngine.InputSystem;

public class ClosePannel : MonoBehaviour
{
    public InputActionProperty aButtonAction; // Exposed in inspector to assign the A button
    void Update()
    {
        if (aButtonAction.action.WasPressedThisFrame())
        {
            Debug.Log(this);
            if (transform.parent != null && transform.parent.parent != null)
            {
                transform.parent.parent.gameObject.SetActive(false);
            }
        }
    }
}

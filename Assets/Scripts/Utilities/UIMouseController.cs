using UnityEngine;

/// <summary>
/// Controls whether the mouse is visible or not. Placed on UI canvases that become active/inactive.
/// </summary>
public class UIMouseController : MonoBehaviour
{
    private void OnEnable()
    {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
    }

	private void OnDisable()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}

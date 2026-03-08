using UnityEngine;

/// <summary>
/// Controls whether the mouse is visible or not. Placed on UI canvases that become active/inactive. Toggles player movement ability
/// </summary>
public class UIMouseController : MonoBehaviour
{
	[SerializeField] private GameObject playerMovementParent;

    private void OnEnable()
    {
		playerMovementParent.SetActive(false);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
    }

	private void OnDisable()
	{
		playerMovementParent.SetActive(true);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}

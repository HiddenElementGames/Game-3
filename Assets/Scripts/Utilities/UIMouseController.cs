using UnityEngine;

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

using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Opens/Closes the inventory UI when the player presses the inventory keybind
/// </summary>
public class InventoryKeybind : MonoBehaviour
{
	[SerializeField] private GameObject inventoryCanvas;

	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
    {
		if ((Keyboard.current.eKey.wasPressedThisFrame || Keyboard.current.iKey.wasPressedThisFrame) && inventoryCanvas.activeSelf == UIManager.Instance.IsMenuOpen)
		{
			inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
			EventManager.Invoke(CustomEventType.MenuToggled);
		}
	}
}

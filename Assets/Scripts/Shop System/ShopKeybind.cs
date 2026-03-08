using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Opens/Closes the shop UI when the player presses the shop keybind
/// </summary>
public class ShopKeybind : MonoBehaviour
{
    [SerializeField] private GameObject shopCanvas;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if(Keyboard.current.tabKey.wasPressedThisFrame && shopCanvas.activeSelf == UIManager.Instance.IsMenuOpen)
        {
			shopCanvas.SetActive(!shopCanvas.activeSelf);
            EventManager.Invoke(CustomEventType.MenuToggled);
        }
    }
}
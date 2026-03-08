using UnityEngine;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;

    private bool isMenuOpen = false; // tracks whether there is currently a menu

	public bool IsMenuOpen { get { return isMenuOpen; } }

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	private void OnEnable()
	{
		// subscribe to events
		EventManager.StartListening(CustomEventType.MenuToggled, OnMenuToggled);
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive
	/// </summary>
	private void OnDisable()
	{
		// unsubscribe from events
		EventManager.StopListening(CustomEventType.MenuToggled, OnMenuToggled);
	}

	/// <summary>
	/// Start is called before the first frame update
	/// </summary>
	private void Start()
	{
		Instance = this;
	}

	private void OnMenuToggled()
	{
		isMenuOpen = !isMenuOpen;
	}
}

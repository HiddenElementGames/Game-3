using UnityEngine;
using UnityEngine.InputSystem;

public class BlockPlacementSystem : MonoBehaviour
{
    [SerializeField] private Transform placementStartPoint;
	[SerializeField] private AudioSource blockPlacementSound;

    private GameObject selectedBlock = null;

    // used for checking if player is in the way of block placement. Value is roughly half the size of a block
    private Vector3 halfExtents = new Vector3(0.49f, 0.49f, 0.49f);

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	private void OnEnable()
	{
		// subscribe to events
		EventManager.StartListening<GameObject>(CustomEventType.BlockSelected, OnBlockSelected);
		EventManager.StartListening(CustomEventType.BlockDeselected, OnBlockDeselected);
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive
	/// </summary>
	private void OnDisable()
	{
		// unsubscribe from events
		EventManager.StopListening<GameObject>(CustomEventType.BlockSelected, OnBlockSelected);
		EventManager.StopListening(CustomEventType.BlockDeselected, OnBlockDeselected);
	}

	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	private void Update()
    {
        if(Mouse.current.rightButton.wasPressedThisFrame && PlayerCanPlaceBlock())
        {
            PlaceBlock();
        }
    }

	/// <summary>
	/// Checks if the player can place the block. Used for any data related issues, not spatial issues.
	/// </summary>
	/// <returns>Boolean value for whether the player can place this block</returns>
	private bool PlayerCanPlaceBlock()
	{
		return selectedBlock != null;
	}

	/// <summary>
	/// Places a block in front of the player, against the surface of the block they are looking at. Checks to ensure the space is valid.
	/// </summary>
    private void PlaceBlock()
    {
        if(Physics.Raycast(placementStartPoint.position, placementStartPoint.forward, out RaycastHit hitInfo))
        {
            // calculate the spawn position
            Vector3 spawnPosition = new Vector3(Mathf.RoundToInt(hitInfo.point.x + hitInfo.normal.x / 2), 
                                                Mathf.RoundToInt(hitInfo.point.y + hitInfo.normal.y / 2), 
                                                Mathf.RoundToInt(hitInfo.point.z + hitInfo.normal.z / 2));

			// ensure the player is not in the way, before spawning the block
			if (Physics.OverlapBox(spawnPosition, halfExtents).Length == 0)
            {
				blockPlacementSound.PlayOneShot(blockPlacementSound.clip);
				Instantiate(selectedBlock, spawnPosition, Quaternion.identity);
				EventManager.Invoke(CustomEventType.BlockPlaced);
			}
        }
    }

	/// <summary>
	/// Updates the selected block for the player
	/// </summary>
	/// <param name="selectedBlockPrefab">The new block the player has selected</param>
	private void OnBlockSelected(GameObject selectedBlockPrefab)
	{
		selectedBlock = selectedBlockPrefab;
	}

	private void OnBlockDeselected()
	{
		selectedBlock = null;
	}
}

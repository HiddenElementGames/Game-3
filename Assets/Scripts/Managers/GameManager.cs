using UnityEngine;

/// <summary>
/// Manages the state of the player and game
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton

    private BlockData selectedBlock; // tracks which block the player currently has selected, for placement

	/// <summary>
	/// Start is called once before the first execution of Update after the MonoBehaviour is created
	/// </summary>
	private void Start()
    {
        Instance = this;
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        // subscribe to events
        EventManager.StartListening<BlockData>(CustomEventType.BlockSelected, OnBlockSelected);
        EventManager.StartListening(CustomEventType.BlockPlaced, OnBlockPlaced);
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive
    /// </summary>
    private void OnDisable()
    {
        // unsubscribe from events
        EventManager.StopListening<BlockData>(CustomEventType.BlockSelected, OnBlockSelected);
        EventManager.StopListening(CustomEventType.BlockPlaced, OnBlockPlaced);
    }

    /// <summary>
    /// Tracks the current selected block and passes along relevant info to other systems
    /// </summary>
    /// <remarks>Called when the player selects a block for placement</remarks>
    /// <param name="blockData">The data for the selected block</param>
    private void OnBlockSelected(BlockData blockData)
    {
        selectedBlock = blockData;

		// passes the prefab along to the BlockPlacementSystem.cs
		EventManager.Invoke(CustomEventType.BlockSelected, blockData.BlockPrefab);
    }

    /// <summary>
    /// Called after a block is placed. Announces the type of block
    /// </summary>
    private void OnBlockPlaced()
    {
        EventManager.Invoke(CustomEventType.BlockPlaced, selectedBlock);
    }
}

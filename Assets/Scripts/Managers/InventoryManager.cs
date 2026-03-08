using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Records the players inventory for their block counts
/// </summary>
public class InventoryManager : MonoBehaviour
{
	public static InventoryManager Instance;

    private Dictionary<BlockData, int> blockCounts = new(); // inventory count for each block type

	public Dictionary<BlockData, int> BlockCounts { get { return blockCounts; } }

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	private void OnEnable()
	{
		// subscribe to events
		EventManager.StartListening<BlockData>(CustomEventType.BlockPurchased, OnBlockPurchased);
		EventManager.StartListening<BlockData>(CustomEventType.BlockPlaced, OnBlockPlaced);
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive
	/// </summary>
	private void OnDisable()
	{
		// unsubscribe from events
		EventManager.StopListening<BlockData>(CustomEventType.BlockPurchased, OnBlockPurchased);
		EventManager.StopListening<BlockData>(CustomEventType.BlockPlaced, OnBlockPlaced);
	}

	/// <summary>
	/// Start is called before the first frame update
	/// </summary>
	private void Start()
	{
		Instance = this;
	}

	/// <summary>
	/// Adds purchased blocks to the inventory
	/// </summary>
	/// <param name="blockData">The block data being purchased</param>
	private void OnBlockPurchased(BlockData blockData)
	{
		if(!blockCounts.ContainsKey(blockData))
		{
			blockCounts.Add(blockData, 0);
		}
		blockCounts[blockData]++;
	}

	/// <summary>
	/// Removes placed blocks from the inventory
	/// </summary>
	/// <param name="blockData">The block data being placed</param>
	private void OnBlockPlaced(BlockData blockData)
	{
		blockCounts[blockData]--;
		if (blockCounts[blockData] == 0)
		{
			EventManager.Invoke(CustomEventType.BlockDeselected);
			blockCounts.Remove(blockData);
		}
	}

	/// <summary>
	/// Checks if the player has the selected block
	/// </summary>
	/// <returns>True if the player currently has the selected block</returns>
	public bool HasBlock()
	{
		return blockCounts.ContainsKey(GameManager.Instance.SelectedBlock);
	}

	/// <summary>
	/// Checks if the player has the provided block
	/// </summary>
	/// <param name="blockData">The block being checked if the player currently has</param>
	/// <returns>True if the player currently has the provided block</returns>
	public bool HasBlock(BlockData blockData)
	{
		return blockCounts.ContainsKey(blockData);
	}
}

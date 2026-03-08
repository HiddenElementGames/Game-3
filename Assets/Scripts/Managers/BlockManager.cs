using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static BlockManager Instance;

    public BlockData[] BlockDatas
    {
        get
        {
            return blockDatas;
        }
    }

    [SerializeField] private BlockData[] blockDatas;

    private Dictionary<BlockData, int> blockCounts = new();
    private Dictionary<BlockData, WaitForSeconds> blockGainIntervals = new();

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	private void OnEnable()
    {
        EventManager.StartListening<BlockData>(CustomEventType.BlockPlaced, OnBlockPlaced);
    }

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	private void OnDisable()
    {
        EventManager.StopListening<BlockData>(CustomEventType.BlockPlaced, OnBlockPlaced);
    }

    /// <summary>
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    /// </summary>
    private void Start()
    {
        Instance = this;

        foreach(BlockData blockData in blockDatas)
        {
            blockCounts.Add(blockData, 0);
            blockGainIntervals.Add(blockData, new WaitForSeconds(blockData.BlockGainTime));
            StartCoroutine(ResourceGainLoop(blockData));
        }
    }

    /// <summary>
    /// A coroutine that endlessly cycles, giving the player resources every cycle
    /// </summary>
    /// <param name="blockData">The block data that is generating resources</param>
    private IEnumerator ResourceGainLoop(BlockData blockData)
    {
        while(true)
        {
            yield return blockGainIntervals[blockData];
            ResourceManager.Instance.GainResources(blockData.BlockType, blockData.BlockGainAmount * blockCounts[blockData]);
        }
    }

    /// <summary>
    /// Increases the block count for the placed block
    /// </summary>
    /// <param name="blockData">The block data for the block being placed</param>
    private void OnBlockPlaced(BlockData blockData)
    {
        blockCounts[blockData]++;
    }
}

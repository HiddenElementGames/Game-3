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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;

        foreach(BlockData blockData in blockDatas)
        {
            blockCounts.Add(blockData, 0);
            blockGainIntervals.Add(blockData, new WaitForSeconds(blockData.BlockGainTime));
            StartCoroutine(ResourceGainLoop(blockData));
        }
    }

    private IEnumerator ResourceGainLoop(BlockData blockData)
    {
        while(true)
        {
            yield return blockGainIntervals[blockData];
            ResourceManager.Instance.GainResources(blockData.BlockType, blockData.BlockGainAmount);
        }
    }
}

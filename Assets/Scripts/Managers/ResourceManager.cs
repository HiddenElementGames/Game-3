using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    private Dictionary<BlockType, float> resourceAmounts = 
        new Dictionary<BlockType, float> { 
            { BlockType.None, 0 },
            { BlockType.Farming, 0 },
            { BlockType.Mining, 0 },
            { BlockType.Enchanting, 0 } };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }

    public void GainResources(BlockType blockType, float gainAmount)
    {
        resourceAmounts[blockType] += gainAmount;
    }
}

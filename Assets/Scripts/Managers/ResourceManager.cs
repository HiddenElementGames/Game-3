using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    private Dictionary<BlockType, float> resourceAmounts = 
        new Dictionary<BlockType, float> { 
            { BlockType.Farming, 0 },
            { BlockType.Mining, 0 },
            { BlockType.Arcane, 0 } };

    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
        EventManager.StartListening<BlockType>(CustomEventType.ResourcesRequested, OnResourcesRequested);
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnDisable()
    {
        EventManager.StopListening<BlockType>(CustomEventType.ResourcesRequested, OnResourcesRequested);
    }

    /// <summary>
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    /// </summary>
    void Start()
    {
        Instance = this;
    }

    private void AnnounceResourcesAvailable(BlockType blockType)
    {
        EventManager.Invoke(CustomEventType.ResourcesAnnounced, (blockType, resourceAmounts[blockType]));
    }

    private void OnResourcesRequested(BlockType blockType)
    {
        AnnounceResourcesAvailable(blockType);
    }

    public void GainResources(BlockType blockType, float gainAmount)
    {
        resourceAmounts[blockType] += gainAmount;
        AnnounceResourcesAvailable(blockType);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelSystem : MonoBehaviour
{
    private Dictionary<BlockType, int> blockLevels = new(); // tracks levels for each block type
    private Dictionary<BlockType, float> blockExperiences = new(); // tracks current experience for each block type
    private Dictionary<BlockType, int> upgradePoints = new(); // tracks available upgrade points

    private const float BASE_EXPERIENCE_FOR_LEVEL_UP = 500f;
    private const float EXPERIENCE_SCALING_AMOUNT_PER_LEVEL = 2f;

    private void OnEnable()
    {
        EventManager.StartListening<(BlockType, float)>(CustomEventType.ResourcesGenerated, OnResourcesGenerated);
    }

    private void OnDisable()
    {
		EventManager.StopListening<(BlockType, float)>(CustomEventType.ResourcesGenerated, OnResourcesGenerated);
	}

    /// <summary>
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    /// </summary>
    void Start()
    {
        // Add starting point for each progression type
        blockLevels.Add(BlockType.Mining, 0);
		blockLevels.Add(BlockType.Farming, 0);
		blockLevels.Add(BlockType.Arcane, 0);
		blockExperiences.Add(BlockType.Mining, 0);
		blockExperiences.Add(BlockType.Farming, 0);
		blockExperiences.Add(BlockType.Arcane, 0);
		upgradePoints.Add(BlockType.Mining, 0);
		upgradePoints.Add(BlockType.Farming, 0);
		upgradePoints.Add(BlockType.Arcane, 0);
	}

    private void OnResourcesGenerated((BlockType blockType, float gainAmount) generatedResources)
    {
        blockExperiences[generatedResources.blockType] += generatedResources.gainAmount;
        CheckForLevelUp(generatedResources.blockType);
    }

    private void CheckForLevelUp(BlockType blockType)
    {
        float requiredExperience = BASE_EXPERIENCE_FOR_LEVEL_UP + (BASE_EXPERIENCE_FOR_LEVEL_UP * EXPERIENCE_SCALING_AMOUNT_PER_LEVEL * blockLevels[blockType]);
        float currentExperience = blockExperiences[blockType];
        if(currentExperience >= requiredExperience)
        {
            blockLevels[blockType]++;
            upgradePoints[blockType]++;
        }
    }
}

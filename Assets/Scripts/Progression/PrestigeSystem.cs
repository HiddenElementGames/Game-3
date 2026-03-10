using System.Collections.Generic;
using UnityEngine;

public class PrestigeSystem : MonoBehaviour
{
    private Dictionary<BlockType, bool> blockPrestigeAvailable = new();

    private void Start()
    {
        blockPrestigeAvailable.Add(BlockType.Farming, false);
		blockPrestigeAvailable.Add(BlockType.Mining, false);
		blockPrestigeAvailable.Add(BlockType.Arcane, false);
	}

	private void OnEnable()
	{
		EventManager.StartListening<BlockType>(CustomEventType.PrestigeAvailable, OnPrestigeAvailable);
	}

	private void OnDisable()
	{
		EventManager.StopListening<BlockType>(CustomEventType.PrestigeAvailable, OnPrestigeAvailable);
	}

	private void OnPrestigeAvailable(BlockType blockType)
	{
		blockPrestigeAvailable[blockType] = true;
	}
}

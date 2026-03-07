using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    private Dictionary<BlockData, TextMeshProUGUI> blockCostTexts = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBlockCostText(BlockData blockData, TextMeshProUGUI costText)
    {
        blockCostTexts.Add(blockData, costText);
    }
}

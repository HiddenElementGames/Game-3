using UnityEngine;

[CreateAssetMenu(fileName = "Block Data", menuName = "Block Data")]
public class BlockData : ScriptableObject
{
    [SerializeField, Tooltip("The display name of the block in game.")] private string blockName;
    [SerializeField, Tooltip("The prefab for the block in game.")] private GameObject blockPrefab;
    [SerializeField, Tooltip("The type of the block, in relation to \"xp\" or resource gain.")] private BlockType blockType;
    [SerializeField, Tooltip("The rate amount at which the block generates resources or xp.")] private int blockGainRate;
    [SerializeField, Tooltip("The time interval at which the block generates resources or xp.")] private int blockGainTime;
    [SerializeField, Tooltip("The BASE cost to buy the block.")] private int blockCost;
    [SerializeField, Tooltip("The scale type for the block cost.\nNone: No scaling.\nLinear: Cost increases linearly.\nMultiplicative: Cost increases multiplicatively.\nExponential: Cost increases exponentially.")] private ScaleType blockCostScaleType;
}

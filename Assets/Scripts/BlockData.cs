using UnityEngine;

[CreateAssetMenu(fileName = "Block Data", menuName = "Block Data")]
public class BlockData : ScriptableObject
{
    [SerializeField, Tooltip("The display name of the block in game.")] private string blockName;
    [SerializeField, Tooltip("The prefab for the block in game.")] private GameObject blockPrefab;
    [SerializeField, Tooltip("The type of the block, in relation to \"xp\" or resource gain.")] private BlockType blockType;
    [SerializeField, Tooltip("The rate amount at which the block generates resources or xp.")] private float blockGainAmount;
    [SerializeField, Tooltip("The time interval at which the block generates resources or xp.")] private float blockGainTime;
    [SerializeField, Tooltip("The BASE cost to buy the block.")] private float blockCost;
    [SerializeField, Tooltip("The scale type for the block cost.\nNone: No scaling.\nLinear: Cost increases linearly.\nMultiplicative: Cost increases multiplicatively.\nExponential: Cost increases exponentially.")] private ScaleType blockCostScaleType;
    [SerializeField, Tooltip("The sprite for the item display in the shop")] private Sprite blockSprite;

    public string BlockName { get { return blockName; } }
    public GameObject BlockPrefab { get { return blockPrefab; } }
    public BlockType BlockType { get { return blockType; } }
    public float BlockGainAmount {  get { return blockGainAmount; } }
    public float BlockGainTime { get { return blockGainTime; } }
    public float BlockCost { get { return blockCost; } }
    public ScaleType BlockCostScaleType { get { return blockCostScaleType; } }
    public Sprite BlockSprite { get { return blockSprite; } }
}

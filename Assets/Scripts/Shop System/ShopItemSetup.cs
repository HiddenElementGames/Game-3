using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Sets up this shop item
/// </summary>
public class ShopItemSetup : MonoBehaviour
{
    [SerializeField] private BlockData blockData;
	[SerializeField] private TextMeshProUGUI displayNameText;
	[SerializeField] private Button purchaseButton;
	[SerializeField] private Image itemImage;
	[SerializeField] private TextMeshProUGUI itemCostText;

	/// <summary>
	/// Start is called once before the first execution of Update after the MonoBehaviour is created
	/// </summary>
	private void Start()
    {
		this.name = "Shop Item: " + blockData.name;
        displayNameText.text = blockData.BlockName;
        purchaseButton.onClick.AddListener(()=> EventManager.Invoke(CustomEventType.BlockPurchased, blockData));
        itemImage.sprite = blockData.BlockSprite;
        itemCostText.text = "Cost: " + blockData.BlockCost.ToString();

        ShopManager.Instance.AddBlockCostText(blockData, itemCostText);
    }
}

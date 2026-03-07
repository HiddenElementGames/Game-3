using UnityEngine;
using System.Collections;

/// <summary>
/// Initializes and sets up the shop UI
/// </summary>
public class InitializeShopUI : MonoBehaviour
{
    [SerializeField] private GameObject shopBlockPrefab;

	/// <summary>
	/// Start is called once before the first execution of Update after the MonoBehaviour is created
	/// </summary>
	private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        yield return null;

        // set up a shop item for every block, at runtime
        foreach(BlockData blockData in BlockManager.Instance.BlockDatas)
        {
            GameObject shopBlock = Instantiate(shopBlockPrefab, transform);

            ShopItemSetupData blockSetup = shopBlock.GetComponent<ShopItemSetupData>();

            blockSetup.DisplayNameText.text = blockData.BlockName;
            blockSetup.PurchaseButton.onClick.AddListener(()=> EventManager.Invoke(CustomEventType.BlockSelected, blockData));
            blockSetup.ItemImage.sprite = blockData.BlockSprite;
            blockSetup.ItemCostText.text = blockData.BlockCost.ToString();

            ShopManager.Instance.AddBlockCostText(blockData, blockSetup.ItemCostText);
        }
    }
}

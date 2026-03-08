using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCurrencyUI : MonoBehaviour
{
    [SerializeField] private Image currencyIconImage;
    [SerializeField] private Sprite farmingCurrencySprite;
    [SerializeField] private Sprite miningCurrencySprite;
    [SerializeField] private Sprite arcaneCurrencySprite;
    [SerializeField] private Button farmingCategoryButton;
    [SerializeField] private Button miningCategoryButton;
    [SerializeField] private Button arcaneCategoryButton;
    [SerializeField] private TextMeshProUGUI currencyText;

    private BlockType displayCurrencyType = BlockType.Farming;

    private void Start()
    {
        farmingCategoryButton.onClick.AddListener(() => EventManager.Invoke(CustomEventType.ShopCategorySwitched, (BlockType.Farming, farmingCurrencySprite)));
		miningCategoryButton.onClick.AddListener(() => EventManager.Invoke(CustomEventType.ShopCategorySwitched, (BlockType.Mining, miningCurrencySprite)));
		arcaneCategoryButton.onClick.AddListener(() => EventManager.Invoke(CustomEventType.ShopCategorySwitched, (BlockType.Arcane, arcaneCurrencySprite)));
	}

    private void OnEnable()
    {
        EventManager.StartListening<(BlockType, float)>(CustomEventType.ResourcesAnnounced, OnResourcesAnnounced);
        EventManager.StartListening<(BlockType, Sprite)>(CustomEventType.ShopCategorySwitched, OnShopCategorySwitched);
        EventManager.Invoke(CustomEventType.ResourcesRequested, displayCurrencyType);
    }

    private void OnDisable()
    {
        EventManager.StopListening<(BlockType, float)>(CustomEventType.ResourcesAnnounced, OnResourcesAnnounced);
        EventManager.StopListening<(BlockType, Sprite)>(CustomEventType.ShopCategorySwitched, OnShopCategorySwitched);
    }

    private void OnResourcesAnnounced((BlockType blockType, float resourceAmount) announcedResource)
    {
        if(announcedResource.blockType == displayCurrencyType)
        {
            currencyText.text = Mathf.FloorToInt(announcedResource.resourceAmount).ToString();
        }
    }

    private void OnShopCategorySwitched((BlockType blockType, Sprite currencySprite) category)
    {
        displayCurrencyType = category.blockType;
        EventManager.Invoke(CustomEventType.ResourcesRequested, category.blockType);
        currencyIconImage.sprite = category.currencySprite;
    }
}

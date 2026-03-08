using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryItemData[] itemDatas;

    private void OnEnable()
    {
        int index = 0;
        foreach(KeyValuePair<BlockData, int> kvp in InventoryManager.Instance.BlockCounts)
        {
            InventoryItemData itemData = itemDatas[index];
            itemData.gameObject.SetActive(true);
            itemData.ItemNameText.text = kvp.Key.BlockName;
            itemData.ItemImage.sprite = kvp.Key.BlockSprite;
            itemData.ItemCountText.text = kvp.Value.ToString();
            itemData.ItemButton.onClick.RemoveAllListeners();
            itemData.ItemButton.onClick.AddListener(() => EventManager.Invoke(CustomEventType.BlockSelected, kvp.Key));
            index++;
        }

        for(int i = index; i < itemDatas.Length; i++)
        {
            itemDatas[i].gameObject.SetActive(false);
        }
    }
}

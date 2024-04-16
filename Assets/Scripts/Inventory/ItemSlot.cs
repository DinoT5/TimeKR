using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData itemData;

    private InventoryView _viewController;
    
    private Image _spawnedItemSprite;

    public void OnSelect(BaseEventData eventData)
    {
        _viewController.OnSlotSelected(this);
    }

    public bool IsEmpty()
    {
        return itemData == null;
    }

    private void OnEnable()
    {
        _viewController = FindObjectOfType<InventoryView>();

        if (itemData == null) return;

        _spawnedItemSprite = Instantiate<Image>(itemData.Sprite, transform.position, Quaternion.identity, transform);
    }
    private void OnDisable()
    {
        if (_spawnedItemSprite != null)
        {
            Destroy(_spawnedItemSprite);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceUsed : MonoBehaviour
{
    [SerializeField] private ItemData _requiredItem;
    private void OnEnable()
    {
        EventBus.Instance.onItemUsed += OnItemUsed;
    }

    // Update is called once per frame
    private void OnDisable()
    {
        EventBus.Instance.onItemUsed -= OnItemUsed;

    }
    private void OnItemUsed(ItemData item)
    {

        if (Vector3.Distance(PlayerMovement.Instance.transform.position, transform.position) < 3)
        {

            if (item == _requiredItem)
            {
                if (PlayerPrefs.HasKey("EnemiesSpawned"))
                {
                    PlayerPrefs.SetInt("EnemiesSpawned",PlayerPrefs.GetInt("EnemiesSpawned") -1);
                }
                else
                {
                PlayerPrefs.SetInt("EnemiesSpawned",8);
                }
                InventoryView.Instance.CloseInventory();
                if (item.DestroyAfterUse)
                {
                    InventoryView.Instance.GetSelectedSlot().itemData = null;
                }
            }
        }
    }
}

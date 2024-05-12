using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceUsed : MonoBehaviour
{
    [SerializeField] private ItemData _requiredItem;
    [SerializeField] private TextAsset inkJSON_ItemGET;
    public AudioSource src;
    public AudioClip _defenseUsed;


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

        if (Vector3.Distance(PlayerMovement.Instance.transform.position, transform.position) < 8)
        {

            if (item == _requiredItem)
            {
                InventoryView.Instance.CloseInventory();
                if (item.DestroyAfterUse)
                {
                    InventoryView.Instance.GetSelectedSlot().itemData = null;
                }
                src.clip = _defenseUsed;
                src.Play();
                Actions.OrderCurrencyUpdate.InvokeAction(-5);
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON_ItemGET);

                if (PlayerPrefs.HasKey("EnemiesSpawned"))
                {
                    PlayerPrefs.SetInt("EnemiesSpawned",PlayerPrefs.GetInt("EnemiesSpawned") -1);
                }
                else
                {
                PlayerPrefs.SetInt("EnemiesSpawned",6);
                }
                

                
            }
        }
    }
}

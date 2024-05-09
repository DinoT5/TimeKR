using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGive : MonoBehaviour
{

    [SerializeField] private ItemData _requiredItem;
    [SerializeField] private ItemData itemData;
    [SerializeField] private TextAsset inkJSON_ItemGET;
    public DialogueTrigger _dialogueKey;
    [SerializeField] private GameObject visualCue;



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
                EventBus.Instance.PickUpItem(itemData);
                InventoryView.Instance.CloseInventory();
                if (item.DestroyAfterUse)
                {
                    InventoryView.Instance.GetSelectedSlot().itemData= null;
                }
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON_ItemGET);
                visualCue.SetActive(false);
                _dialogueKey.enabled = false;


            }

        }
    }
}

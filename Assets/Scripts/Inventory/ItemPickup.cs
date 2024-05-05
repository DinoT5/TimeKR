using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
     public GameObject visualCue;

    private void Start()
    {
        visualCue.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        visualCue.SetActive(true);
        if (Input.GetKey(KeyCode.E))
        {
            if (InventoryView.Instance.state == InventoryView.State.menuOpen)
            {
                return;
            }
            EventBus.Instance.PickUpItem(itemData);
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit(Collider Player)
    {
        visualCue.SetActive(false);
    }
}

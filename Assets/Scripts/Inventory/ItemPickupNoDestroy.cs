using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupNoDestroy : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    private void Start()
    {
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (Input.GetKey(KeyCode.E))
        {
            EventBus.Instance.PickUpItem(itemData);
            
        }
    }
    private void OnTriggerExit(Collider Player)
    {

    }
}

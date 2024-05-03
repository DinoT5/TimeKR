using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemNameText;
    [SerializeField] private TMP_Text _itemDescriptionText;
    [SerializeField] private GameObject _inventoryViewObject;
    [SerializeField] private PlayerController _characterMove;
    
    [SerializeField] private List<ItemSlot> _slots;

    [SerializeField] private ItemSlot _currentSlot;
    
    [SerializeField] private ScreenFader _fader;
    
    [SerializeField] private  GameObject _contextMenuObject;
    [SerializeField] private GameObject _firstContextMenuOption;

    [SerializeField] private List<Button> _contextMenuIgnore;


    private void Start()
    {
        _slots[0].GetComponent<Button>().onClick.AddListener(() => _currentSlot = _slots[0]);
        _slots[1].GetComponent<Button>().onClick.AddListener(() => _currentSlot = _slots[1]);
        _slots[2].GetComponent<Button>().onClick.AddListener(() => _currentSlot = _slots[2]);
        _slots[3].GetComponent<Button>().onClick.AddListener(() => _currentSlot = _slots[3]);
        _slots[4].GetComponent<Button>().onClick.AddListener(() => _currentSlot = _slots[4]);
        _slots[5].GetComponent<Button>().onClick.AddListener(() => _currentSlot = _slots[5]);
        _slots[6].GetComponent<Button>().onClick.AddListener(() => _currentSlot = _slots[6]);
        _slots[7].GetComponent<Button>().onClick.AddListener(() => _currentSlot = _slots[7]);
        _slots[8].GetComponent<Button>().onClick.AddListener(() => _currentSlot = _slots[8]);
        _slots[9].GetComponent<Button>().onClick.AddListener(() => _currentSlot = _slots[9]);
        /*        for (int i = 0; i < _slots.Count; i++)
                {
                    _slots[i].GetComponent<Button>().onClick.AddListener(() => _currentSlot = _slots[i]);
                }*/
    }
    private enum State
    {
        menuClosed,

        menuOpen,

        contextMenu,
    }
    private State _state;

    public void UseItem()
    {
        Debug.Log(_currentSlot);
        _fader.FadeFromBlack(1f,FadeToUseItemCallback);

    }

    private void FadeToUseItemCallback()
    {
        _contextMenuObject.SetActive(false);
        //_inventoryViewObject.SetActive(false);
        _fader.FadeFromBlack(1f, () => EventBus.Instance.UseItem(_currentSlot.itemData));
        //_state = State.menuClosed;
    }

    private void OnEnable()
    {
        EventBus.Instance.onPickUpItem += OnItemPickedUp;
    }
    private void OnDisable()
    {
        EventBus.Instance.onPickUpItem -= OnItemPickedUp;
    }
    private void OnItemPickedUp(ItemData itemData)
    {
        foreach (var slot in _slots)
        {
            if (slot.IsEmpty())
            {
                slot.itemData = itemData;
                break;
            }
        }
    }
    public void OnSlotSelected(ItemSlot selectedSlot)
    {

        _currentSlot = selectedSlot;
        if (selectedSlot.itemData == null)
        {
            
            _itemNameText.ClearMesh();
            _itemDescriptionText.ClearMesh();
            return; 
        }
        _itemNameText.SetText(selectedSlot.itemData.Name);
        _itemDescriptionText.SetText(selectedSlot.itemData.Description[0]);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (_state == State.menuClosed)
            {
                EventBus.Instance.PauseGameplay();
                _inventoryViewObject.SetActive(true);
                _state = State.menuOpen;
            }
            else if (_state == State.menuOpen)
            {
                EventBus.Instance.ResumeGameplay();
                _inventoryViewObject.SetActive(false);
                _state = State.menuClosed;

            }
            else if (_state == State.contextMenu)
            {
                _contextMenuObject.SetActive(false);
                foreach (var button in _contextMenuIgnore)
                {
                    button.interactable = true;
                }
                EventSystem.current.SetSelectedGameObject(_currentSlot.gameObject);
                _state = State.menuOpen;
            }

        }
        //context menu
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_state == State.menuOpen)
            {
                if (EventSystem.current.currentSelectedGameObject.TryGetComponent<ItemSlot>(out var slot))
                {
                    _state = State.contextMenu;
                    _contextMenuObject.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(_firstContextMenuOption);
                    foreach (var button in _contextMenuIgnore)
                    {
                        button.interactable = false;
                    }
                }
            }
        }


    }

}

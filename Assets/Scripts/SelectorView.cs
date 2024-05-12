using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectorView : MonoBehaviour
{
    [SerializeField] private float _speed = 25f;
    private RectTransform _rectTransform;
    private GameObject _selected;
    public GameObject _defaultSelected;
    
    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }


    void Update()
    {
        var selectedGameObject = EventSystem.current.currentSelectedGameObject;

        if ((selectedGameObject == null || !selectedGameObject.CompareTag("SelectorViewSelectable")))
        {
            _selected = _selected;
        
        }
        else if (!selectedGameObject.activeInHierarchy)
        {
            _selected = _defaultSelected;
        }
        else
        {
            _selected = selectedGameObject;
        }

        EventSystem.current.SetSelectedGameObject(_selected);
        
        if (_selected == null) return;

        transform.position = Vector3.Lerp(transform.position, _selected.transform.position, _speed * Time.deltaTime);

        var otherRect = _selected.GetComponent<RectTransform>();

        var horizontalLerp = Mathf.Lerp(_rectTransform.rect.size.x, otherRect.rect.size.x, _speed * Time.deltaTime);
        var verticalLerp = Mathf.Lerp(_rectTransform.rect.size.y, otherRect.rect.size.y, _speed * Time.deltaTime);

        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, horizontalLerp);
        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, verticalLerp);

    }
}

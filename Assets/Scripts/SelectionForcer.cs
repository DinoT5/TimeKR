using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionForcer : MonoBehaviour
{
    private GameObject _previousSelection;

        public GameObject defaultSelectedObject; 

    void Start()
    {
        if (defaultSelectedObject != null)
        {
            EventSystem.current.SetSelectedGameObject(defaultSelectedObject);
        }
    }

    private void Update()
    {
        var currentSelection = EventSystem.current.currentSelectedGameObject;
        if (currentSelection != null)
        {
            _previousSelection = currentSelection;
        }

        if (currentSelection == null)
        {
            EventSystem.current.SetSelectedGameObject(_previousSelection);
        }
    }
}

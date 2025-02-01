using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform _inventoryPanel;
    [SerializeField] private Status _player;

    private bool _isPaused = false;
    private List<Slot> _slots = new List<Slot>();
    private bool _isOpened = false;

    public Status Player => _player;

    private void Start()
    {
        _inventoryPanel.parent.gameObject.SetActive(false);

        for (int i = 0; i < _inventoryPanel.childCount; i++)
        {
            if (_inventoryPanel.GetChild(i).GetComponent<Slot>() != null)
            {
                _slots.Add(_inventoryPanel.GetChild(i).GetComponent<Slot>());
            }
        }
    }

    private void Update()
    {
        if  (Input.GetKeyDown(KeyCode.I))
        {
            _isOpened = !_isOpened;

            if (_isOpened)
            {
                ToggleInventory();
            }
            else
            {
                ToggleInventory();
            }
        }
    }

    private void ToggleInventory()
    {
        _isPaused = !_isPaused;

        Time.timeScale = _isPaused ? 0f : 1f;

        Cursor.visible = _isPaused;
        Cursor.lockState = _isPaused ? CursorLockMode.None : CursorLockMode.Locked;

        GameObject inventory = _inventoryPanel.parent.gameObject;
        inventory.SetActive(_isPaused);
    } 

    public void AddItem(ICollectable item)
    {
        foreach(Slot slot in _slots)
        {
            if (slot.IsEmpty)
            {
                slot.AddItem(item);
                slot.GetComponent<Image>().sprite = item.Icon;

                return;
            }
        }
    }

    public void RemoveItem(ICollectable item)
    {
        foreach (Slot slot in _slots)
        {
            if (slot.Item == item)
            {
                slot.RemoveItem();
                item.Destroy();
            }
         }
    }

    public bool HasFreeSlot()
    {
        foreach (Slot slot in _slots)
        {
            if (slot.Item == null)
            {
                return true;
            }
        }

        return false;
    }
}

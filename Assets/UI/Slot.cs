using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private ICollectable _item = null;
    [SerializeField] private SelectedItem _panel;

    private bool _isEmpty = true;

    public ICollectable Item => _item;
    public bool IsEmpty => _isEmpty;

    public void AddItem(ICollectable item)
    {
        _item = item;
        _isEmpty = false;
    }

    public void OnSlotClick()
    {
        if (_item != null)
        {
            _panel.SetItem(_item);
        }
    }

    public void RemoveItem()
    {
        _item = null;
        GetComponent<Image>().sprite = null;
    }
}

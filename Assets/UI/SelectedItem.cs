using UnityEngine;
using UnityEngine.UI;

public class SelectedItem : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _useButton;
    [SerializeField] private Button _removeButton;

    private ICollectable _item = null;

    private void Start()
    {
        gameObject.SetActive(false);

        _useButton.onClick.AddListener(UseItem);
        _removeButton.onClick.AddListener(RemoveItem);
    }

    private void ClosePanel()
    {
        _item = null;

        gameObject.SetActive(false);
    }

    private void UseItem()
    {
        _item.Use();

        ClosePanel();
    }

    private void RemoveItem()
    {
        _item.Remove();

        ClosePanel();
    }

    public void SetItem(ICollectable item)
    {
        _item = item;

        _image.sprite = _item.Icon;
        _image.color = new Color(1, 1, 1, 1);

        gameObject.SetActive(true);
    }

    
}

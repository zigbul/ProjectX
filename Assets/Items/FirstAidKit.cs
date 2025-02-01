using UnityEngine;
using UnityEngine.UI;

public class FirstAidKit : MonoBehaviour, ICollectable
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private float _healAmount = 100;

    public string Name => _name;
    public Sprite Icon => _icon;

   public void Collect()
    {
        if (_inventory.HasFreeSlot())
        {
            _inventory.AddItem(this);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("В инвентаре нет места.");
        }
    }

    public void Use()
    {
        _inventory.Player.ChangeHealth(_healAmount);

        Remove();
    }

    public void Remove()
    {
        _inventory.RemoveItem(this);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}

using UnityEngine;

public interface ICollectable
{
    public string Name { get; }
    public Sprite Icon { get; }

    public void Collect();

    public void Use();

    public void Remove();

    public void Destroy();
}

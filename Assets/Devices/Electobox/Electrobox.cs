using UnityEngine;

public class Electrobox : MonoBehaviour, IInterectable
{
    [SerializeField] private Status _player;

    private float _energyToRestoreAmount = 100f;

    public void Interact()
    {
        _player.ChangeEnergy(_energyToRestoreAmount);
    }
}

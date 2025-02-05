using UnityEngine;

public class Status : MonoBehaviour
{
    private float _health = 100f;
    private float _maxHealth = 100f;

    private float _energy = 100f;
    private float _maxEnergy = 100f;

    public float Health => _health;
    public float Energy => _energy;

    public void ChangeHealth(float value)
    {
        if (Mathf.Sign(value) == -1)
        {
            GetComponent<PlayerSound>().PlayPainSound();
        }

        _health += value;

        if (_health <= 0)
        {
            _health = 0;
        }

        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    public void ChangeEnergy(float value)
    {
        _energy += value;

        if (_energy < 0)
        {
            _energy = 0;
        }

        if (_energy > _maxEnergy)
        {
            _energy = _maxEnergy;
        }
    }
}

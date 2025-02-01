using UnityEngine;
using UnityEngine.UI;

public class Indicators : MonoBehaviour
{
    [SerializeField] private Status _player;
    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _energyBar;

    private float _secondsToLostEnergy = 60f;

    void Start()
    {
        _healthBar.fillAmount = _player.Health / 100;
        
    }

    void Update()
    {
        _player.ChangeEnergy(-100 / _secondsToLostEnergy * Time.deltaTime);
        DrawEnergyBar();
        DrawHealthBar();
    }

    private void DrawEnergyBar()
    {
        _energyBar.fillAmount = Mathf.Lerp(_energyBar.fillAmount, _player.Energy / 100, Time.deltaTime);
    }

    private void DrawHealthBar()
    {
        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, _player.Health / 100, 10f * Time.deltaTime);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class GameUiManager : MonoBehaviour
{
    [Header("Game UI Settings")]

    [Header("Hud")]
    [SerializeField] private GameObject _hudPanel;
    [SerializeField] private Slider _healthSlider;

    [Header("References")]
    [SerializeField] private UnitStatHandler _playerUnitStatHandler;

    public void UpdateHealthSlider()
    {
        _healthSlider.value = _playerUnitStatHandler.CurrentHealth;
    }

    private void Update()
    {
        UpdateHealthSlider();
    }

    private void Awake()
    {
        _hudPanel.SetActive(true);

        _healthSlider.maxValue = _playerUnitStatHandler.MaxHealth;
        _healthSlider.value = _playerUnitStatHandler.CurrentHealth;
    }
}

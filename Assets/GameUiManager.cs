using UnityEngine;
using UnityEngine.UI;

public class GameUiManager : MonoBehaviour
{
    [Header("Game UI Settings")]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _gameWinPanel;

    [Header("Hud")]
    [SerializeField] private GameObject _hudPanel;
    [SerializeField] private Slider _healthSlider;

    [Header("References")]
    [SerializeField] private UnitStatHandler _playerUnitStatHandler;

    public void ShowGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }

    public void ShowGameWinPanel()
    {
        _gameWinPanel.SetActive(true);
    }

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
        _gameOverPanel.SetActive(false);
        _gameWinPanel.SetActive(false);
        _hudPanel.SetActive(true);

        _healthSlider.maxValue = _playerUnitStatHandler.MaxHealth;
        _healthSlider.value = _playerUnitStatHandler.CurrentHealth;
    }
}

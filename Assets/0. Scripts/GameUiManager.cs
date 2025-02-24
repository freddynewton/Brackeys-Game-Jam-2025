using System;
using UnityEngine;
using UnityEngine.UI;

public class GameUiManager : MonoBehaviour
{
    [Header("Game UI Settings")]

    [Header("Hud")]
    [SerializeField] private GameObject _hudPanel;
    [SerializeField] private Slider _healthSlider;

    [Header("Menu")]
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _MainMenuButton;

    [Header("Game Over")]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Button _restartGameOverButton;
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private Button _quitGameOverButton;

    [Header("References")]
    [SerializeField] private UnitStatHandler _playerUnitStatHandler;

    public void UpdateHealthSlider()
    {
        if (_playerUnitStatHandler == null)
        {
            return;
        }

        _healthSlider.value = _playerUnitStatHandler.CurrentHealth;
    }

    public void ShowGameOver()
    {
        _gameOverPanel.SetActive(true);
    }

    private void Update()
    {
        if (_playerUnitStatHandler == null)
        {
            return;
        }

        UpdateHealthSlider();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _menuPanel.SetActive(true);
            Time.timeScale = 0;
        }

        if (_playerUnitStatHandler.CurrentHealth <= 0)
        {
            ShowGameOverScreen();
        }
    }

    private void Awake()
    {
        _hudPanel.SetActive(true);
        _menuPanel.SetActive(false);
        _gameOverPanel.SetActive(false);

        _healthSlider.maxValue = _playerUnitStatHandler.MaxHealth;
        _healthSlider.value = _playerUnitStatHandler.CurrentHealth;

        _resumeButton.onClick.AddListener(() => ResumeGame());
        _MainMenuButton.onClick.AddListener(() => MainMenu());

        _quitButton.onClick.AddListener(() => Application.Quit());

        _restartGameOverButton.onClick.AddListener(() => Restart());
        _backToMenuButton.onClick.AddListener(() => MainMenu());
        _quitGameOverButton.onClick.AddListener(() => Application.Quit());
    }

    private void Restart()
    {
        GameSceneFlowManager.Instance.ReloadCurrentScene();
    }

    private void MainMenu()
    {
        Time.timeScale = 1;
        GameSceneFlowManager.Instance.LoadScene("Main Menu", false);
    }

    private void ResumeGame()
    {
        _menuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void ShowGameOverScreen()
    {
        _gameOverPanel.SetActive(true);
    }
}

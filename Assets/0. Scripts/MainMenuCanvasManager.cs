using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvasManager : MonoBehaviour
{
    [SerializeField] private RectTransform _barryPortrait;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _backButton;

    [SerializeField] private Slider _optionsSlider;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _optionsMenu;

    private void Start()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _quitButton.onClick.AddListener(OnQuitButtonClicked);
        _backButton.onClick.AddListener(OnBackbuttonPressed);

        //_optionsSlider.onValueChanged(SoundManager.Instance.);
    }

    private void Awake()
    {
        // clear all click listeners
        _playButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
        _backButton.onClick.RemoveAllListeners();

        // add new click listeners
        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _quitButton.onClick.AddListener(OnQuitButtonClicked);
        _backButton.onClick.AddListener(OnBackbuttonPressed);

        // set the position of the Barry portrait with DoTween up and down
        _barryPortrait.DOAnchorPosY(-50f, 3f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    private void OnSettingsButtonClicked()
    {
        _optionsMenu.SetActive(true);
        _mainMenu.SetActive(false);
    }

    private void OnPlayButtonClicked()
    {
        GameSceneFlowManager.Instance.LoadScene("Level 0", true);
    }

    private void OnBackbuttonPressed()
    {
        _optionsMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }

}

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

    private void Start()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void Awake()
    {
        // clear all click listeners
        _playButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();

        // add new click listeners
        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _quitButton.onClick.AddListener(OnQuitButtonClicked);

        // set the position of the Barry portrait with DoTween up and down
        _barryPortrait.DOAnchorPosY(-50f, 3f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    private void OnSettingsButtonClicked()
    {
        Debug.Log("Settings button clicked");
    }

    private void OnPlayButtonClicked()
    {
        GameSceneFlowManager.Instance.LoadScene("Level 0", true);
    }
}

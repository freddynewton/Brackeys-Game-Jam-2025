using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Collections;

public class DialogPanelManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float dialogSpeed = 0.1f;
    [SerializeField] private List<string> _dialog;

    [Header("References")]
    [SerializeField] private CanvasGroup dialogPanel;
    [SerializeField] private CanvasGroup arrow;

    [SerializeField] private RectTransform _barryPortrait;

    [SerializeField] private TMPro.TextMeshProUGUI _dialogText;

    private Vector2 _barryPortraitPosition;
    private float _currentDialogSpeed;

    private bool _isTyping;

    private void Awake()
    {
        dialogPanel.alpha = 0;
        dialogPanel.interactable = false;
        dialogPanel.blocksRaycasts = false;
        _dialogText.text = string.Empty;
        arrow.alpha = 0;

        _barryPortraitPosition = _barryPortrait.anchoredPosition;
        _barryPortrait.gameObject.SetActive(false);

        Show(_dialog);
    }

    private void Update()
    {
        if (Input.anyKeyDown && !_isTyping)
        {
            if (_dialog.Count > 0)
            {
                StartCoroutine(FadeCanvasGroup(arrow, 0, 0.5f));
                StartCoroutine(TypewriterEffect(_dialog[0]));
            }
            else
            {
                StartCoroutine(FadeCanvasGroup(dialogPanel, 0, 0.5f));
                InputManager.Instance.SetPlayerInputActive(true);
            }
        }
    }

    public void Show(List<string> dialog)
    {
        InputManager.Instance.SetPlayerInputActive(false);
        gameObject.SetActive(true);

        _dialog = dialog;

        // Custom tweening alpha
        StartCoroutine(FadeCanvasGroup(dialogPanel, 1, 3f));
        StartCoroutine(TypewriterEffect(dialog[0]));

        _barryPortrait.anchoredPosition = new Vector2(_barryPortraitPosition.x - 500, _barryPortraitPosition.y);

        _barryPortrait.gameObject.SetActive(true);
        _barryPortrait.DOAnchorPos(_barryPortraitPosition, 2).SetEase(Ease.OutBack);
        _barryPortrait.DOAnchorPosY(_barryPortraitPosition.y + 10, 3).SetEase(Ease.OutBack).SetLoops(-1, LoopType.Yoyo);
    }

    private IEnumerator TypewriterEffect(string dialogLine)
    {
        _dialogText.text = "";
        _dialog.RemoveAt(0);

        _isTyping = true;

        // Append one character at a time to simulate typing
        foreach (char c in dialogLine)
        {
            _dialogText.text += c;
            yield return new WaitForSeconds(dialogSpeed);
        }

        // Show the arrow
        StartCoroutine(FadeCanvasGroup(arrow, 1, 1f));

        _isTyping = false;
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float targetAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}

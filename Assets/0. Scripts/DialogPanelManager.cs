using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class DialogPanelManager : MonoBehaviour
{
    [Header("Dialog Settings")]
    [SerializeField] private float dialogSpeed = 0.1f;
    [SerializeField] private float _startDelay = 2f;
    [SerializeField] private List<DialogElement> _dialog;

    [Header("References")]
    [SerializeField] private CanvasGroup dialogPanel;
    [SerializeField] private CanvasGroup arrow;

    [SerializeField] private RectTransform _barryPortrait;
    [SerializeField] private RectTransform _apprenticePortrait;

    [SerializeField] private TMPro.TextMeshProUGUI _nameText;
    [SerializeField] private TMPro.TextMeshProUGUI _dialogText;

    [Header("Extra Settings")]
    [SerializeField] private bool _LoadSceneOnEnd;
    [SerializeField] private string _sceneToLoad;

    private Vector2 _barryPortraitPosition;
    private Vector2 _apprenticePortraitPosition;

    private bool _isTyping;

    private void Awake()
    {
        dialogPanel.alpha = 0;
        dialogPanel.interactable = false;
        dialogPanel.blocksRaycasts = false;
        _dialogText.text = string.Empty;
        arrow.alpha = 0;

        _barryPortraitPosition = _barryPortrait.anchoredPosition;
        _apprenticePortraitPosition = _apprenticePortrait.anchoredPosition;
        _barryPortrait.gameObject.SetActive(false);
        _apprenticePortrait.gameObject.SetActive(false);

        InputManager.Instance.SetPlayerInputActive(false);

        StartCoroutine(Show(_dialog));
    }

    private void Update()
    {
        if (Input.anyKeyDown && !_isTyping)
        {
            if (_dialog.Count > 0)
            {
                UpdatePortraitImage();

                StartCoroutine(FadeCanvasGroup(arrow, 0, 0.5f));
                StartCoroutine(TypewriterEffect(_dialog[0].Dialog));
            }
            else
            {
                StartCoroutine(FadeCanvasGroup(dialogPanel, 0, 0.5f));
                InputManager.Instance.SetPlayerInputActive(true);

                if (_LoadSceneOnEnd)
                {
                    _LoadSceneOnEnd = false;
                    GameSceneFlowManager.Instance.LoadScene(_sceneToLoad, true);
                }
            }
        }
    }

    private void UpdatePortraitImage()
    {
        if (_dialog.Count == 0)
        {
            return;
        } 

        if (_dialog[0].Character == DialogCharacter.Barry)
        {
            _nameText.text = "Broomstick Barry";

            _barryPortrait.DOScale(Vector3.one * 1.2f, 0.33f).SetEase(Ease.OutBack);
            _apprenticePortrait.DOScale(Vector3.one, 0.33f).SetEase(Ease.OutBack);

            _barryPortrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            _apprenticePortrait.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
        else if (_dialog[0].Character == DialogCharacter.Apprentice)
        {
            _nameText.text = "Dirty Dan";

            _apprenticePortrait.DOScale(Vector3.one * 1.2f, 0.33f).SetEase(Ease.OutBack);
            _barryPortrait.DOScale(Vector3.one, 0.33f).SetEase(Ease.OutBack);

            _apprenticePortrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            _barryPortrait.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
    }

    public IEnumerator Show(List<DialogElement> dialog)
    {
        yield return new WaitForSeconds(_startDelay);

        UpdatePortraitImage();

        InputManager.Instance.SetPlayerInputActive(false);
        gameObject.SetActive(true);

        _dialog = dialog;

        // Custom tweening alpha
        StartCoroutine(FadeCanvasGroup(dialogPanel, 1, 3f));
        StartCoroutine(TypewriterEffect(dialog[0].Dialog));

        // Check if the Character is in the dialog with linq
        if (dialog.Any(dialog => dialog.Character == DialogCharacter.Barry))
        {
            _barryPortrait.anchoredPosition = new Vector2(_barryPortraitPosition.x - 500, _barryPortraitPosition.y);
            _barryPortrait.gameObject.SetActive(true);
            _barryPortrait.DOAnchorPos(_barryPortraitPosition, 2).SetEase(Ease.OutBack);
            _barryPortrait.DOAnchorPosY(_barryPortraitPosition.y + 10, 3).SetEase(Ease.OutBack).SetLoops(-1, LoopType.Yoyo);
        }

        if (dialog.Any(dialog => dialog.Character == DialogCharacter.Apprentice))
        {
            _apprenticePortrait.anchoredPosition = new Vector2(_apprenticePortraitPosition.x + 500, _apprenticePortraitPosition.y);
            _apprenticePortrait.gameObject.SetActive(true);
            _apprenticePortrait.DOAnchorPos(_apprenticePortraitPosition, 2).SetEase(Ease.OutBack);
            _apprenticePortrait.DOAnchorPosY(_apprenticePortraitPosition.y + 10, 3).SetEase(Ease.OutBack).SetLoops(-1, LoopType.Yoyo);
        }

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

[System.Serializable]
public class DialogElement
{
    public string Dialog;

    public DialogCharacter Character;

    public DialogElement(string name, string dialog, DialogCharacter character)
    {
        Dialog = dialog;
        this.Character = character;
    }
}

public enum DialogCharacter
{
    Barry,
    Apprentice
}

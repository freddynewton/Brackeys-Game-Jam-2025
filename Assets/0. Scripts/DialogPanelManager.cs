using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;

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

    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _dialogText;

    [Header("Extra Settings")]
    [SerializeField] private bool _LoadSceneOnEnd;
    [SerializeField] private string _sceneToLoad;

    private Vector2 _barryPortraitPosition;
    private Vector2 _apprenticePortraitPosition;
    private int _currentDialogIndex;

    private Image _barryPortraitImage;
    private Image _apprenticePortraitImage;

    private Task _typeWriterTask;

    private void Awake()
    {
        _barryPortraitImage = _barryPortrait.GetComponent<Image>();
        _apprenticePortraitImage = _apprenticePortrait.GetComponent<Image>();

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
        if (_typeWriterTask == null)
        {
            return;
        }

        if (Input.anyKeyDown && _typeWriterTask.IsCompleted)
        {
            if (_currentDialogIndex < _dialog.Count)
            {
                UpdatePortraitImage();

                StartCoroutine(FadeCanvasGroup(arrow, 0, 0.5f));
                _typeWriterTask = TypewriterEffectAsync(_dialog[_currentDialogIndex].Dialog);
            }
            else
            {
                StartCoroutine(FadeCanvasGroup(dialogPanel, 0, 0.5f));
                InputManager.Instance.SetPlayerInputActive(true);

                SoundManager.Instance.SetLevelMusicVolume(0.3f);

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
        if (_currentDialogIndex >= _dialog.Count)
        {
            return;
        }

        _barryPortrait.DOKill(true);
        _apprenticePortrait.DOKill(true);

        if (_dialog[_currentDialogIndex].Character == DialogCharacter.Barry)
        {
            _nameText.text = "Broomstick Barry";
            _barryPortrait.DOScale(Vector3.one * 1.2f, 0.33f).SetEase(Ease.OutBack);
            _apprenticePortrait.DOScale(Vector3.one, 0.33f).SetEase(Ease.OutBack);

            _barryPortraitImage.color = new Color(1, 1, 1, 1);
            _apprenticePortraitImage.color = new Color(1, 1, 1, 0.5f);

            SoundManager.Instance.PlayDanTalk();
        }
        else if (_dialog[_currentDialogIndex].Character == DialogCharacter.Apprentice)
        {
            _nameText.text = "Dustpan Dan";

            _apprenticePortrait.DOScale(Vector3.one * 1.2f, 0.33f).SetEase(Ease.OutBack);
            _barryPortrait.DOScale(Vector3.one, 0.33f).SetEase(Ease.OutBack);

            _apprenticePortraitImage.color = new Color(1, 1, 1, 1);
            _barryPortraitImage.color = new Color(1, 1, 1, 0.5f);
        }
    }

    public IEnumerator Show(List<DialogElement> dialog)
    {
        if (_dialog.Count == 0)
        {
            yield break;
        }

        yield return new WaitForSeconds(_startDelay);

        UpdatePortraitImage();

        InputManager.Instance.SetPlayerInputActive(false);
        gameObject.SetActive(true);

        _dialog = dialog;

        SoundManager.Instance.SetLevelMusicVolume(0.05f);

        // Custom tweening alpha
        StartCoroutine(FadeCanvasGroup(dialogPanel, 1, 3f));
        _typeWriterTask = TypewriterEffectAsync(dialog[0].Dialog);

        _barryPortrait.DOKill(true);
        _apprenticePortrait.DOKill(true);

        // Check if the Character is in the dialog with linq
        if (dialog.Any(dialog => dialog.Character == DialogCharacter.Barry))
        {

            //_barryPortrait.anchoredPosition = new Vector2(_barryPortraitPosition.x - 500, _barryPortraitPosition.y);
            _barryPortrait.gameObject.SetActive(true);

            _barryPortrait.DOAnchorPos(_barryPortraitPosition, 2).SetEase(Ease.OutBack);
            _barryPortrait.DOAnchorPosY(_barryPortraitPosition.y + 10, 3).SetEase(Ease.OutBack).SetLoops(-1, LoopType.Yoyo);
        }

        if (dialog.Any(dialog => dialog.Character == DialogCharacter.Apprentice))
        {
            // _apprenticePortrait.anchoredPosition = new Vector2(_apprenticePortraitPosition.x + 500, _apprenticePortraitPosition.y);
            _apprenticePortrait.gameObject.SetActive(true);
            _apprenticePortrait.DOAnchorPos(_apprenticePortraitPosition, 2).SetEase(Ease.OutBack);
            _apprenticePortrait.DOAnchorPosY(_apprenticePortraitPosition.y + 10, 3).SetEase(Ease.OutBack).SetLoops(-1, LoopType.Yoyo);
        }

    }

    private async Task TypewriterEffectAsync(string dialogLine)
    {
        _dialogText.text = "";
        // Append one character at a time to simulate typing
        foreach (char c in dialogLine)
        {
            _dialogText.text += c;
            await Task.Delay((int)(dialogSpeed * 1000));

            if (_dialog[_currentDialogIndex].Character == DialogCharacter.Apprentice)
            {
                SoundManager.Instance.PlayApTalk();
            }
            else
            {
                SoundManager.Instance.PlayDanTalk();
            }

            // Show the arrow
            StartCoroutine(FadeCanvasGroup(arrow, 1, 1f));
        }

        _currentDialogIndex++;

        // return the task
        return;
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

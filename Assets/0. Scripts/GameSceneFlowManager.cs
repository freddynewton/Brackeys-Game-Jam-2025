using System.Collections.Generic;
using System.Threading.Tasks;
using TransitionsPlus;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneFlowManager : Singleton<GameSceneFlowManager>
{
    [Header("Transition Settings")]
    [SerializeField] private TransitionAnimator _transitionAnimator;
    [SerializeField] private float _transitionDuration = 2f;

    private List<string> _currentScenes = new List<string>();
    private string _sceneToLoad;
    private bool _isLoading = false;

    private const string _bootScene = "_Boot";

    public void LoadScene(string sceneName, bool isLevel, float duration = 0.5f)
    {
        if (_isLoading)
        {
            return;
        }

        _isLoading = true;
        _sceneToLoad = sceneName;
        _transitionAnimator.profile.invert = false;
        _transitionAnimator.profile.duration = duration;
        _transitionAnimator.Play();
    }

    public void ReloadCurrentScene()
    {
        if (_isLoading)
        {
            return;
        }

        _isLoading = true;
        _transitionAnimator.profile.invert = false;
        _transitionAnimator.profile.duration = 0.5f;
        _transitionAnimator.Play();
    }

    private async void OnTransitionEnd()
    {
        // Check if the scene name is already in the list of current scenes
        if (_currentScenes.Contains(_sceneToLoad))
        {
            _isLoading = false;
            return;
        }

        // Unload unnecessary scenes
        await UnloadUnnecessaryScenes();

        // Load the new scene
        await SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Additive);

        // Add the scene to the list of current scenes
        _currentScenes.Add(_sceneToLoad);

        // Inverse the transition
        _transitionAnimator.profile.invert = true;
        _transitionAnimator.profile.duration = _transitionDuration;
        _transitionAnimator.Play();

        _isLoading = false;
    }

    private async Task UnloadUnnecessaryScenes()
    {
        // Unload all current scenes except the boot scene
        foreach (string currentScene in _currentScenes)
        {
            if (currentScene != _bootScene)
            {
                await SceneManager.UnloadSceneAsync(currentScene);
            }
        }

        // Clear the list of current scenes except the boot scene
        _currentScenes.Clear();
        _currentScenes.Add(_bootScene);
    }

    public override void Awake()
    {
        base.Awake();
        _transitionAnimator.onTransitionEnd.AddListener(OnTransitionEnd);
        LoadScene("Main Menu", false, 0);
    }
}

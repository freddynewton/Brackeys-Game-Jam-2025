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

    public void LoadScene(string sceneName, bool isLevel, float duration = 2f)
    {
        _sceneToLoad = sceneName;

        _transitionAnimator.profile.invert = false;
        _transitionAnimator.profile.duration = duration;

        // play transition animation
        _transitionAnimator.Play();
    }

    private async void OnTransitionEnd()
    {
        // Check if the scene name is already in the list of current scenes
        if (_currentScenes.Contains(_sceneToLoad))
        {
            return;
        }

        // load the scene
        await UnloadCurrentScenes();
        await SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Additive);

        // add the scene to the list of current scenes
        _currentScenes.Add(_sceneToLoad);

        // inverse the transition
        _transitionAnimator.profile.invert = true;
        _transitionAnimator.profile.duration = _transitionDuration;
        _transitionAnimator.Play();
    }

    private async Task UnloadCurrentScenes()
    {
        // unload all current scenes
        foreach (string currentScene in _currentScenes)
        {
            await SceneManager.UnloadSceneAsync(currentScene);
        }

        // clear the list of current scenes
        _currentScenes.Clear();
    }

    public override async void Awake()
    {
        base.Awake();

        _transitionAnimator.onTransitionEnd.AddListener(OnTransitionEnd);

        // load the first scene
        LoadScene("Main Menu", false, 0);
    }
}

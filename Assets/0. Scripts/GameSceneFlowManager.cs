using System.Collections.Generic;
using TransitionsPlus;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneFlowManager : Singleton<GameSceneFlowManager>
{
    [Header("Transition Settings")]
    [SerializeField] private TransitionAnimator _transitionAnimator;
    [SerializeField] private float _transitionDuration = 2f;

    private List<string> _currentScenes = new List<string>();

    public async void LoadScene(string sceneName)
    {
        // play transition animation
        _transitionAnimator.Play();

        // load the scene
        await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        // add the scene to the list of current scenes
        _currentScenes.Add(sceneName);
    }

    public override void Awake()
    {
        base.Awake();

        LoadScene("Main Menu");
    }
}

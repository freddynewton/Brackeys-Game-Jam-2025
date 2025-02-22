using MoreMountains.Feedbacks;
using System.Collections.Generic;
using UnityEngine;

public enum GameFeelType
{
    FreezeGame,
    SmallShake,
    BigShake,
    PostProcessHit
}

public class GameFeelManager : Singleton<GameFeelManager>
{
    [Header("Game Feel Settings")]
    [SerializeField] private MMF_Player _freezeGameFeedback;
    [SerializeField] private MMF_Player _smallShakeFeedback;
    [SerializeField] private MMF_Player _bigShakeFeedback;
    [SerializeField] private MMF_Player _postProcessHitFeedback;

    private Dictionary<GameFeelType, MMF_Player> _gameFeelFeedbacks;

    public override void Awake()
    {
        base.Awake();
        _gameFeelFeedbacks = new Dictionary<GameFeelType, MMF_Player>
        {
            { GameFeelType.FreezeGame, _freezeGameFeedback },
            { GameFeelType.SmallShake, _smallShakeFeedback },
            { GameFeelType.BigShake, _bigShakeFeedback },
            { GameFeelType.PostProcessHit, _postProcessHitFeedback }
        };
    }

    public void PlayGameFeel(GameFeelType gameFeelType)
    {
        if (_gameFeelFeedbacks.ContainsKey(gameFeelType))
        {
            _gameFeelFeedbacks[gameFeelType].PlayFeedbacks();
        }
    }
}

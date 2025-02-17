using UnityEngine;

/// <summary>
/// Manages the main player functionalities.
/// </summary>
public class PlayerMainManager : Singleton<PlayerMainManager>
{
    private PlayerMainController _playerMainController;

    public PlayerMainController PlayerMainController
    {
        get
        {
            if (_playerMainController == null)
            {
                _playerMainController = FindFirstObjectByType<PlayerMainController>();
            }

            return _playerMainController;
        }
        private set => _playerMainController = value;
    }
}

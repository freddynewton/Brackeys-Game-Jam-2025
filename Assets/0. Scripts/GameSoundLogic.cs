using UnityEngine;

public class GameSoundLogic : MonoBehaviour
{
    private void Awake()
    {
        SoundManager.Instance.PlayLevelMusic();
    }
}

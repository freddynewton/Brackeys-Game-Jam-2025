using System.Collections;
using UnityEngine;

public class GameSoundLogic : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(waitForSeconds());
    }

    private IEnumerator waitForSeconds()
    {
        yield return new WaitForSeconds(0.2f);
        SoundManager.Instance.PlayLevelMusic();
    }
}

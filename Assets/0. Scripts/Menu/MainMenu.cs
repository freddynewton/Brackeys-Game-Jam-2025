using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [Range(0,100)][SerializeField] private int scene = 1;

    public void PlayGame()
    {
        SceneManager.LoadScene(scene);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button soundButton;
    [SerializeField] private Sprite soundOff, soundOn;

    public void PlayGame()
    {
        GameManager.instance.gameStartedFromMainMenu = true;
        SceneManager.LoadScene(Tags.GAMEPLAY_SCENE);
    }

//    public void Sound()
//    {
//    }
//
//    public void Options()
//    {
//    }

    public void ControlMusic()
    {
        if (GameManager.instance.canPlayMusic)
        {
            soundButton.image.sprite = soundOn;
            GameManager.instance.canPlayMusic = false;
        }
        else
        {
            soundButton.image.sprite = soundOff;
            GameManager.instance.canPlayMusic = true;
        }
    }
}
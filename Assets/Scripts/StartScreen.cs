using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            LoadMainGame();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            ExitGame();
        }
    }

    void LoadMainGame()
    {
        SceneManager.LoadScene("Game");
    }

    void ExitGame()
    {
        Application.Quit();
    }
}

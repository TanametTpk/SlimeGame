using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;

    public void TogglePause(InputAction.CallbackContext value) {
        if (!value.started) return;

        if (isPaused) {
            Resume();
        }else {
            Pause();
        }
    }

    public void Resume() {
        PlayerInput input = FindObjectOfType<PlayerInput>();
        input.SwitchCurrentActionMap("Player");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause() {
        PlayerInput input = FindObjectOfType<PlayerInput>();
        input.SwitchCurrentActionMap("UI");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu() {
        LevelLoader loader = FindObjectOfType<LevelLoader>();
        Resume();
        StartCoroutine(loader.LoadLevel(0));
    }

    public void QuitGame() {
        Debug.Log("Outting game...");
        Application.Quit();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Home() {
        LoadScene(0);
    }

    public void PlayGame() {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        LoadScene(sceneIndex);
    }

    public void PlayAgain() {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        LoadScene(sceneIndex);
    }

    public void GotoGame() {
        LoadScene(1);
    }

    public void GotoWin() {
        LoadScene(3);
    }

    public void EndScene() {
        // same logic just make it's fu*king work!!!
        PlayGame();
    }

    private void LoadScene(int index) {
        SceneManager.LoadScene(index);
    }

    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }
}

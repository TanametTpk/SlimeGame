using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCheck : MonoBehaviour
{
    public GameObject coinManager;
    public RPGCharacter player;
    private MainMenu menu;

    private void Start() {
        menu = FindObjectOfType<MainMenu>();
    }

    private void Update() {
        if (player.GetHealth() <= 0) {
            ShowLose();
        }

        if (IsWin()) {
            ShowWin();
        }
    }

    private void ShowLose() {
        AudioManager.instance.StopAll();
        menu.EndScene();
    }

    private void ShowWin() {
        AudioManager.instance.StopAll();
        menu.GotoWin();
    }

    private bool IsWin() {
        return coinManager.transform.childCount < 1;
    }
}

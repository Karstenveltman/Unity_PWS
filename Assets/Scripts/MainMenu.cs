using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //opens singleplayer screen
    public void PlayGame(){
        SceneManager.LoadScene(1);
    }
    //opens multiplayer screen and stores info that is hosting
    public void PlayMultiplayerAsHost() {
        PlayerPrefs.SetInt("PlayerNr", 0);
        SceneManager.LoadScene(2);
    }
    //opens multiplayer screen and stores info that is joining
    public void PlayMultiplayerAsClient() {
        PlayerPrefs.SetInt("PlayerNr", 1);
        SceneManager.LoadScene(2);
    }
    //quits game fully
    public void QuitGame(){
        Application.Quit();
    }
    //goes back to menu from gameover screen
    public void BackToMenu(){
        SceneManager.LoadScene(0);
    }
}

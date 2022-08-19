using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public void newGame() {
       SceneManager.LoadScene("gameLevel");
    }

    public void gameOverScene() {
        SceneManager.LoadScene("failScreen");
    }

    public void menu() {
        SceneManager.LoadScene("menu");
    }

    public void exit() {
        Application.Quit();
    }

    public void developer() {
        SceneManager.LoadScene("developerScreen");
    }

    public void resetRec() {
        string recP = Application.persistentDataPath + "/" + "record" + ".txt";

        if (File.Exists(recP)) {
            File.WriteAllText(recP, "0");
        }

        SceneManager.LoadScene("menu");
    }
}

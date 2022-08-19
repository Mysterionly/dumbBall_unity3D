using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class userDataIO : MonoBehaviour {
    
    public Text scoreLable;

	private string recP;
	private string lastP;
	private string savingP;
	private string settingsP;

    public string recVal;
    public string lastVal;

    // Use this for initialization
    void Start () {
		recP = Application.persistentDataPath + "/" + "record" + ".txt";
		lastP = Application.persistentDataPath + "/" + "last" + ".txt";


        recVal = readFromFile(recP);
        lastVal = readFromFile(lastP);

        scoreLable.text = lastVal + "\n" + recVal;
    }

    public string readFromFile(string path) {
        string output = "";

        if (File.Exists(path)) output = File.ReadAllText(path);
        else {
            StreamWriter sw = System.IO.File.CreateText(path);
            sw.Close();
            File.WriteAllText(path, "0");
            output = "0";
        }

        return output;
    }
}

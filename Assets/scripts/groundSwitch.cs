using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class groundSwitch : MonoBehaviour
{

    public GameObject clone;
    public Text score;

    //public Transform inHoleCheck;
    //public Transform newHoleCheck;
    public Transform bottomCollideCheck;

    public LayerMask ball;
    public LayerMask bottom;

	public GameObject holeModel;
	public GameObject PauseMenu;
	public GameObject Ball;
	public GameObject NewRec;

    public float bottomCollideRadius = 0.6f;
    public float groundRadius = 0.4f;
    public int gameCouner = 0;

    public bool gameOver;
    public bool inHole;
    public bool newHole, removeAndPlace = false;
	public bool pause = false;

    private BoxCollider gCol;

    public Text scoreLable;

    public string recP;
    public string lastP;

	private int recVal;

    // Use this for initialization
    void Start()
    {
        gCol = GetComponent<BoxCollider>();
        //clone = (GameObject)Instantiate(holeModel, new Vector3(UnityEngine.Random.Range(-2.4f, 9.6f), 2.87f, UnityEngine.Random.Range(-12.3f, -1.5f)), Quaternion.Euler(new Vector3(-90, 0, 0)));
		//clone.SetActive (true);inHoleCheck = clone.transform.Find("finish");
        //newHoleCheck = clone.transform.Find("pushSpring");
        
        recP = Application.persistentDataPath + "/" + "record" + ".txt";
        lastP = Application.persistentDataPath + "/" + "last" + ".txt";
}

    // Update is called once per frame
    void Update()
    {
        //inHole = Physics.CheckSphere(inHoleCheck.position, groundRadius, ball);
        //newHole = Physics.CheckSphere(newHoleCheck.position, groundRadius, ball);
        //gameOver = Physics.CheckSphere(bottomCollideCheck.position, bottomCollideRadius, bottom);

		if (Input.GetKeyUp (KeyCode.Escape)) {
			Pause ();
		}

            if (inHole) {
                gCol.enabled = false;
            }
            else {
                gCol.enabled = true;
            }

            if (newHole) {
                removeAndPlace = true;
            }

            if (inHole == false && newHole == false && removeAndPlace == true)
            {
                Destroy(clone);
                clone = (GameObject)Instantiate(holeModel, new Vector3(UnityEngine.Random.Range(-2.4f, 9.6f), 2.87f, UnityEngine.Random.Range(-12.3f, -1.5f)), Quaternion.Euler(new Vector3(-90, 0, 0)));
				clone.SetActive (true);
                //inHoleCheck = clone.transform.Find("finish");
                //newHoleCheck = clone.transform.Find("pushSpring");
                removeAndPlace = false;
                gameCouner++;
                score.text = gameCouner.ToString();
            
                    recVal = int.Parse(File.ReadAllText(recP));
                    File.WriteAllText(lastP, gameCouner.ToString());
			if (gameCouner > recVal) {
				File.WriteAllText (recP, gameCouner.ToString ());
				NewRec.SetActive (true);
			}
        }

        if (gameOver)
        {
            File.WriteAllText(lastP, gameCouner.ToString());
            SceneManager.LoadScene("failScreen");
        }
    }

	public void Pause(){

		if (!pause)
			pause = true;
		else
			pause = false;
		
		if (pause) {
			PauseMenu.SetActive (true);
			Ball.SetActive (false);
			clone.SetActive (false);
		}

		else {
			PauseMenu.SetActive (false);
			Ball.SetActive (true);
			clone.SetActive (true);
		}
	}


}

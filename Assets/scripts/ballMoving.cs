using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class ballMoving : MonoBehaviour {

    public Transform viewCamera;

	public float xC;
    public float zC;

    public Transform groundCheck;

	//public List<Transform> gCHList = new List<Transform>();

    public Vector3 newFloorPos = new Vector3(3.7f, 4.3f, 11.4f);
    public GameObject floor;

    public Text accelOutX;
    public Text accelOutY;
    public Text accelOutZ;

    public Slider custSpeed;
    public float maxSpeed;

    public LayerMask ground;
    public LayerMask spring;
    public LayerMask trigToGen;

    public float groundRadius = 0.4f;

    public bool grounded;
    public bool pushReady;
    public bool newFloorGen;

    private Rigidbody rB;

    public GameObject player; // тут объект игрока
    private Vector3 offset;
    public GameObject[] newFT;

	// Use this for initialization
	void Start () {
        offset = viewCamera.position - groundCheck.transform.position;
		rB = GetComponent<Rigidbody>();

		if (File.Exists(Application.persistentDataPath + "/" + "settings" + ".txt"))
			custSpeed.value = Convert.ToSingle(File.ReadAllText(Application.persistentDataPath + "/" + "settings" + ".txt"));

		else {
			StreamWriter sw = System.IO.File.CreateText(Application.persistentDataPath + "/" + "settings" + ".txt");
			sw.Close();
			File.WriteAllText(Application.persistentDataPath + "/" + "settings" + ".txt", "15");
			custSpeed.value = 15f;
		}
	}
	
	// Update is called once per frame
	void Update () {
        viewCamera.position = groundCheck.transform.position + offset;
        maxSpeed = custSpeed.value;
        //xC = Input.GetAxis("Horizontal");
        //zC = Input.GetAxis("Vertical");

        xC = Input.acceleration.x;
        zC = -Input.acceleration.z + 0.5f;

		//grounded = Physics.CheckSphere(gCHList[0].position, groundRadius, ground) || Physics.CheckSphere(gCHList[1].position, groundRadius, ground) || Physics.CheckSphere(gCHList[2].position, groundRadius, ground) || Physics.CheckSphere(gCHList[3].position, groundRadius, ground) || Physics.CheckSphere(gCHList[4].position, groundRadius, ground) || Physics.CheckSphere(gCHList[5].position, groundRadius, ground);
        grounded = Physics.CheckSphere(groundCheck.position, groundRadius, ground);
        pushReady = Physics.CheckSphere(groundCheck.position, groundRadius, spring);
        newFloorGen = Physics.CheckSphere(groundCheck.position, groundRadius, trigToGen);

        if (grounded){
            rB.AddForce(xC * maxSpeed, 0, zC * maxSpeed);
        }

        if (newFloorGen)
        {
            newFloorGen = false;
            newFT = GameObject.FindGameObjectsWithTag("newFloorTrigger");
            foreach (GameObject o in newFT){
                Destroy(o);
            }
            Instantiate(floor, newFloorPos, Quaternion.Euler(90.0f, 0.0f, 0.0f));
            newFloorPos.z += 16.55f;
        }

        if (pushReady) {
            rB.velocity = new Vector3(0, 7.0f, 0);
        }
	}


	public void saveSettings(){
		if (File.Exists(Application.persistentDataPath + "/" + "settings" + ".txt"))
			File.WriteAllText(Application.persistentDataPath + "/" + "settings" + ".txt", custSpeed.value.ToString());
	}
}

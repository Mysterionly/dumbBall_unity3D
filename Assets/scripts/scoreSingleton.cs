using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreSingleton
{
    private static scoreSingleton instance = null;
    public static scoreSingleton SharedInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new scoreSingleton();
            }
            return instance;
        }
    }
    public float scoreVal;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton 
{
    static private Singleton Instance;
    static public Singleton GetInstance
    {
        get
        {
            if (Instance == null)
            {
                Instance = new Singleton();
            }
            return Instance;
        }
    }

    //��������
    public eCharacterState cState = eCharacterState.run;

}

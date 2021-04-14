using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour {


    
    private float startTime;
    public static int MoveCloserSteps = 4;

    void Start()
    {
     
    }
    void Update()
    {

    }


    public void MoveMon(GameObject ObjectsToSwitchOnWhenDark)
    {
        var tra = ObjectsToSwitchOnWhenDark.transform;
        var dif = Vector3.ProjectOnPlane(Camera.main.transform.position, Vector3.up) - Vector3.ProjectOnPlane(tra.position, Vector3.up);
        tra.position = tra.position + dif / MoveCloserSteps;
    }


}

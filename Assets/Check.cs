using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.Common;


public class Check : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Frame.Pose.position);
    }

    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("Trigger enter");
    }

    private void OnTriggerExit(Collider other)
    {
            Debug.Log("Trigger exit");
    }

    void OnCollisionStay(Collision collisionInfo) {
     Debug.Log("Trigger col enter");
 }

    void OnCollisionEnter(Collision collisionInfo) {
     Debug.Log("Trigger stay");
 }

    void OnCollisionExit(Collision collisionInfo) {
     Debug.Log("Trigger vella");
 }

}

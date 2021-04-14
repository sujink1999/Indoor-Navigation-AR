using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections.Generic;
using GoogleARCore;
using GoogleARCore.Examples.Common;



public class PlayerController : MonoBehaviour
{
    public Transform[] destinations; // list of destination positions
    public GameObject person; // person indicator
    private NavMeshPath path; // current calculated path
    private LineRenderer line; // linerenderer to display path
    private Transform target; // current chosen destination
    private Transform prevTarget; // current chosen destination
    private bool destinationSet; // bool to say if a destination is set
    public Dropdown dropdown;

    public Transform kitchen;
    public Transform hall;
    public Transform room;


    public GameObject trigger; // collider to change arrows
    public GameObject indicator; // arrow prefab to spawn
    public GameObject arcoreDeviceCam; // ar camera
    public GameObject arrowHelper;
    private Anchor anchor;
    public GameObject marker; 
    private Vector3 markerVector;

    private GameObject spawned_trigger;
    private GameObject spawned_prefab;

    private bool hasEntered; //used for onenter collider, make sure it happens only once
    private bool hasExited;

    private Vector3 anchorPos;

    //create initial path, get linerenderer.
    void Start()
    {
        path = new NavMeshPath();
        line = transform.GetComponent<LineRenderer>();
        destinationSet = false;
        //target = destinations[0];//.position = new Vector3(-1.77f,1f,0.08f);
        prevTarget = null;
        spawned_prefab = null;

        hasEntered = false;
        hasExited = false;
        markerVector = marker.transform.position;
    }

    void Update()
    {
        //Debug.Log(dropdown.value);
        switch (dropdown.value){
            case 1 :  prevTarget = target;
            target = kitchen;                     
                    break;
            case 2 : prevTarget = target;
            target = hall;
                    break;
            case 3 : prevTarget = target;
            target = room;
                    break;
            case 0 : prevTarget = target;
            target = null;
                    line.enabled = false;
                    break;
        }
        //if a target is set, calculate and update path
        if(target != null)
        {
            NavMesh.CalculatePath(person.transform.position, target.position, 
                          NavMesh.AllAreas, path);
            //lost path due to standing above obstacle (drift)
            // line.material = new Material(Shader.Find("Mobile/Particles/Additive"));
            line.material = Resources.Load("Assets/Materials/PathMaterial.mat") as Material;

            if(path.corners.Length == 0)
            {
                Debug.Log("Try moving away for obstacles (optionally recalibrate)");
            }
            line.positionCount = path.corners.Length;
            line.SetPositions(path.corners);

            // A simple 2 color gradient with a fixed alpha of 1.0f.
        // float alpha = 1.0f;
        // Gradient gradient = new Gradient();
        // gradient.SetKeys(
        //     new GradientColorKey[] { new GradientColorKey(startColor, 0.0f), new GradientColorKey(startColor, 1.0f) },
        //     new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        // );
        // line.colorGradient = gradient;

            line.enabled = true;
        } 

        if (prevTarget != target){

                if (spawned_prefab){
                        Destroy(GameObject.Find("Sphere(Clone)"));
                        spawned_prefab = null;
                }
            
            Vector3 node3D = line.GetPosition(1);
            Vector3 diff =  node3D - marker.transform.position;
            Vector3 res = Frame.Pose.position - diff + new Vector3(0,0,20);
            Debug.Log("res : " + res);
            
            Quaternion rot = arcoreDeviceCam.transform.rotation * Quaternion.Euler(20, 180, 0);

            // create new anchors
            anchor = Session.CreateAnchor(new Pose(new Vector3(res.x,0,res.z), rot));
            Debug.Log("ANCHOR DA : " + anchor.transform.position);
            anchorPos = anchor.transform.position;

            //spawn arrow
            spawned_prefab = GameObject.Instantiate(indicator,
                     anchor.transform.position, anchor.transform.rotation, 
                     anchor.transform);
            Debug.Log("Object spawned bro");
        }

        check();
    }


    void check(){
        //     Debug.Log(spawned_prefab.transform.position);
            Vector3 posdiff = anchorPos - Frame.Pose.position - new Vector3(0,0,20);
            Debug.Log("Difference bro "+posdiff);
            if (spawned_prefab){
                    if ((posdiff.x < 0.5f && posdiff.x > -0.5f) && (posdiff.z < 0.5f && posdiff.z > -0.5f)){
                        Vector3 node3D = line.GetPosition(1);
                        Vector3 diff =  node3D - marker.transform.position;
                        Vector3 res = Frame.Pose.position - diff + new Vector3(0,0,20);
                        Debug.Log("res : " + res);
                        Destroy(GameObject.Find("Sphere(Clone)"));
                        spawned_prefab = null;
                        
                        Quaternion rot = arcoreDeviceCam.transform.rotation * Quaternion.Euler(20, 180, 0);

                        // create new anchors
                        anchor = Session.CreateAnchor(new Pose(new Vector3(res.x,0,res.z), rot));
                        Debug.Log("ANCHOR DA : " + anchor.transform.position);
                        anchorPos = anchor.transform.position;

                        //spawn arrow
                        spawned_prefab = GameObject.Instantiate(indicator,
                                anchor.transform.position, anchor.transform.rotation, 
                                anchor.transform);
                        Debug.Log("Object spawned bro");
                    }
            }
    }

        //         //what to do when entering a collider
        // private void OnTriggerEnter(Collider other)
        // {

        //     Debug.Log("Triggered Bro");
        //     //if it is a navTrigger then calculate angle and spawn a new AR arrow
        //     if (other.name.Equals("Cube(Clone)") && line.positionCount > 0)
        //     {
                
        //         Destroy(GameObject.Find("Collider(Clone)"));
        //         Destroy(GameObject.Find("Sphere(Clone)"));

        //         //logic to calculate arrow angle
        //         Vector2 personPos = new Vector2(this.transform.position.x, 
        //                 this.transform.position.z);
        //         Vector2 personHelp = new Vector2(arrowHelper.transform.position.x, 
        //                 arrowHelper.transform.position.z);
        //         Vector3 node3D = line.GetPosition(1);
        //         Vector2 node2D = new Vector2(node3D.x, node3D.z);

        //         float angle = Mathf.Rad2Deg * (Mathf.Atan2(personHelp.y - personPos.y, 
        //                 personHelp.x - personPos.x) - Mathf.Atan2(node2D.y - personPos.y, 
        //                 node2D.x - personPos.x));

        //         // position arrow a bit before the camera and a bit lower
                // Vector3 pos = arcoreDeviceCam.transform.position + 
                //         arcoreDeviceCam.transform.forward * 2 + 
                //         arcoreDeviceCam.transform.up * -0.5f;

        //         // rotate arrow a bit
        //         Quaternion rot = arcoreDeviceCam.transform.rotation * 
        //                 Quaternion.Euler(20, 180, 0);

        //         // create new anchor
        //         anchor = Session.CreateAnchor(new Pose(node3D + new Vector3(0, 0, 100), rot));

        //         //spawn arrow
        //         spawned_prefab = GameObject.Instantiate(indicator, 
        //                 anchor.transform.position, anchor.transform.rotation, 
        //                 anchor.transform);

        //         spawned_trigger = GameObject.Instantiate(trigger, anchor.transform.position, 
        //             anchor.transform.rotation);

                
        //     }     
        // }

        // //what to do when exiting a collider
        // private void OnTriggerExit(Collider other)
        // {
        //     //if it is a navTrigger then delete Anchor and arrow and create a new trigger
        //     if (other.name.Equals("Collider(Clone)"))
        //     {
        //         if (hasExited)
        //         {
        //             return;
        //         }
        //         hasExited = true;
        //         Destroy(GameObject.Find("Collider(Clone)"));
        //         Destroy(GameObject.Find("Sphere(Clone)"));
        //         GameObject.Instantiate(trigger, this.transform.position, 
        //             this.transform.rotation);
        //     }
        // }

    // public NavMeshAgent agent;
    // // Start is called before the first frame update
    // void Start()
    // {
    //     agent.SetDestination(new Vector3(-1.77f,1f,0.08f));
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}

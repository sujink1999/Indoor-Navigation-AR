
    using System.Collections.Generic;
    using GoogleARCore;
    using GoogleARCore.Examples.Common;
    using UnityEngine;
    using UnityEngine.EventSystems;
    public class MarkerController : MonoBehaviour
    {
        public Camera FirstPersonCamera;
        public GameObject CameraTarget;
        private Vector3 PrevARPosePosition;
        private bool Tracking = false;

        /// <summary>
        /// True if the app is in the process of quitting due to an ARCore connection error,
        /// otherwise false.
        /// </summary>
        private bool _isQuitting = false;

        /// <summary>
        /// The Unity Awake() method.
        /// </summary>
        public void Awake()
        {
            // Enable ARCore to target 60fps camera capture frame rate on supported devices.
            // Note, Application.targetFrameRate is ignored when QualitySettings.vSyncCount != 0.
            Application.targetFrameRate = 60;
        }

        public void Start() {
            //set initial position
            PrevARPosePosition = Vector3.zero;
        }

        /// <summary>
        /// The Unity Update() method.
        /// </summary>
        public void Update()
        {
            UpdateApplicationLifecycle();

            //move the person indicator according to position
            Vector3 currentARPosition = Frame.Pose.position;
            if (!Tracking) {
                Tracking = true;
                PrevARPosePosition = Frame.Pose.position;
            }
            //Remember the previous position so we can apply deltas
            Vector3 deltaPosition = currentARPosition - PrevARPosePosition;
            PrevARPosePosition = currentARPosition;
            if (CameraTarget != null) {
            // The initial forward vector of the sphere must be aligned with the initial camera direction in the XZ plane.
            // We apply translation only in the XZ plane.
            CameraTarget.transform.Translate(deltaPosition.x *-1, 0.0f, deltaPosition.z *-1);
            // Set the pose rotation to be used in the CameraFollow script
            //FirstPersonCamera.GetComponent<ArrowDirection>().targetRot = Frame.Pose.rotation;
            }

            // Vector3 targetEulerAngles = Frame.Pose.rotation.eulerAngles;
            // float rotationToApplyAroundY = targetEulerAngles.y;
            // float newCamRotAngleY = Mathf.LerpAngle(arrow.transform.eulerAngles.y, rotationToApplyAroundY, Time.deltaTime);
            // Quaternion newCamRotYQuat = Quaternion.Euler(0, newCamRotAngleY, 0);
            // arrow.transform.rotation = newCamRotYQuat;
        }

        /// <summary>
        /// Check and update the application lifecycle.
        /// </summary>
        private void UpdateApplicationLifecycle()
        {
            // Exit the app when the 'back' button is pressed.
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            // Only allow the screen to sleep when not tracking.
            if (Session.Status != SessionStatus.Tracking)
            {
                Screen.sleepTimeout = SleepTimeout.SystemSetting;
            }
            else
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
            }

            if (_isQuitting)
            {
                return;
            }

            // Quit if ARCore was unable to connect and give Unity some time for the toast to
            // appear.
            if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
            {
                _isQuitting = true;
                Invoke("DoQuit", 0.5f);
            }
            else if (Session.Status.IsError())
            {
                _isQuitting = true;
                Invoke("DoQuit", 0.5f);
            }
        }

        /// <summary>
        /// Actually quit the application.
        /// </summary>
        private void DoQuit()
        {
            Application.Quit();
        }

    }
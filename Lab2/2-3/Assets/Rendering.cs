using UnityEngine;
using UnityEngine.Video;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;
        private VideoPlayer vPlayer;
        private VideoPlayer vPlayer2;

        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            Debug.Log("startup!");
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        void Playvid()
        {
            Debug.Log("prepping vid!");
            GameObject camera = GameObject.Find("ARCamera/StereoCameraLeft");
            GameObject camera2 = GameObject.Find("ARCamera/StereoCameraRight");
            Debug.Log("111");
            vPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>() as VideoPlayer;
            vPlayer2 = camera2.AddComponent<UnityEngine.Video.VideoPlayer>() as VideoPlayer;
            Debug.Log("222");
            vPlayer.url = "http://www.quirksmode.org/html5/videos/big_buck_bunny.mp4";
            vPlayer2.url = "http://www.quirksmode.org/html5/videos/big_buck_bunny.mp4";
            Debug.Log("333");
            vPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
            vPlayer2.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
            Debug.Log("playing vid!");
            vPlayer.Play();
            vPlayer2.Play();
        }

        void Stopvid()
        {
            Debug.Log("stopping vid!");
            vPlayer.Stop();
            vPlayer2.Stop();

        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");

            if (mTrackableBehaviour.TrackableName == "design")
            {
                Debug.Log("design seen!");
                Playvid();
            }
        }




        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");

            if (mTrackableBehaviour.TrackableName == "design")
            {
                Debug.Log("design seen to stop!");
                Stopvid();
            }
        }

        #endregion // PRIVATE_METHODS
    }
}
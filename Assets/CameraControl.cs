using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI; // Required for UI components


public class CameraControl : MonoBehaviour
{
    public GameObject xrRigCamera;      // Reference to your XR Rig Camera
    public GameObject[] timelineCameras; // Array of timeline cameras to disable
    public PlayableDirector director;   // Reference to the Playable Director
    public Button yourButton;


    void Start()
    {
        // Disable timeline cameras at the start
        foreach (var cam in timelineCameras)
        {
            cam.SetActive(false);
        }

        // Ensure XR Rig camera is active
        xrRigCamera.SetActive(true);
        yourButton.onClick.AddListener(OnButtonClick);

    }

    public void PlayTimeline()
    {
        // Enable timeline cameras when the timeline starts
        foreach (var cam in timelineCameras)
        {
            cam.SetActive(true);
        }

        // Deactivate XR Rig camera when the timeline plays (optional)
        xrRigCamera.SetActive(false);

        // Play the timeline
        director.Play();
    }

    // This method will be called when the button is clicked
    void OnButtonClick()
    {
        Debug.Log("Button clicked!");
        PlayTimeline();
    }


}

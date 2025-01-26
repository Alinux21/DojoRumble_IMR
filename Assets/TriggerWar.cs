using UnityEngine;

public class ProximityTrigger : MonoBehaviour
{
    public float rotationSpeed = 10f;    // Speed at which the light rotates
    public Light directionalLight;       // Reference to the directional light
    public bool isRotating = false;      // Flag to indicate whether the sun is rotating

    private Vector3 targetNightRotation = new Vector3(-26f, -103f, -29f); // Target position for the night
    private float rotationThreshold = 10f; // Threshold to stop rotating when close enough to the target rotation
    public AudioClip mp3Clip;            // MP3 audio clip to play
    private AudioSource audioSource;     // Reference to the AudioSource component
    private bool mp3Triggered=false; 

    public Material nightSkybox; // Reference to the night skybox material
    void Start()
    {
        if (directionalLight == null)
        {
            // Automatically find the directional light if not set in the inspector
            directionalLight = FindObjectOfType<Light>();
            if (directionalLight == null || directionalLight.type != LightType.Directional)
            {
                Debug.LogError("No directional light found in the scene!");
            }
        }

        // Get the AudioSource component attached to the same GameObject as the directional light
        audioSource = directionalLight.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // If no AudioSource is attached, log an error and add it
            Debug.LogError("No AudioSource component found on the directional light. Adding one.");
            audioSource = directionalLight.gameObject.AddComponent<AudioSource>();
        }

    }

    void Update()
    {
        if (isRotating)
        {
            // Get current rotation (euler angles)
            Vector3 currentRotation = directionalLight.transform.eulerAngles;


            // Check if we're close enough to the target rotation (within threshold)
            float xDifference = Mathf.Abs(Mathf.DeltaAngle(currentRotation.x, targetNightRotation.x));
            if (xDifference > rotationThreshold)
            {
                // Continue rotating
                currentRotation.x = Mathf.MoveTowardsAngle(currentRotation.x, targetNightRotation.x, rotationSpeed * Time.deltaTime);
                directionalLight.transform.eulerAngles = currentRotation;
            }
            else
            {
                // Stop rotating and finalize rotation
                isRotating = false;
                directionalLight.transform.eulerAngles = targetNightRotation; // Ensure it's exactly at the target rotation
                Debug.Log("Directional light reached the night rotation!");
                StartSkyboxTransition();
                if (audioSource != null && mp3Clip != null && mp3Triggered==false)
                {
                    audioSource.clip = mp3Clip;
                    audioSource.Play();  // Play the audio clip
                    Debug.Log("Playing MP3 file.");
                    mp3Triggered=true;
                }
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Start rotating when the trigger is entered
        isRotating = true;
    }


    void StartSkyboxTransition()
    {
        // Set the skybox blend to the night one when rotation is done
        if (nightSkybox != null)
        {
            // Make sure we switch skyboxes once the transition starts
            RenderSettings.skybox = nightSkybox;
        }
    }

}


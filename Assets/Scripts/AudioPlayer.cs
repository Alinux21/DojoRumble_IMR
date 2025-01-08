using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip testAudioClip; // Assign this in the Inspector for testing

    private void Start()
    {
        Debug.Log("AudioPlayer Start method called."); // Debug log

        if (testAudioClip != null)
        {
            Debug.Log("Playing test audio clip: " + testAudioClip.name); // Debug log
            AudioSource.PlayClipAtPoint(testAudioClip, Camera.main.transform.position);
        }
        else
        {
            Debug.Log("No test audio clip assigned."); // Debug log
        }

        if (SceneData.audioToPlay != null)
        {
            Debug.Log("SceneData.audioToPlay is not null."); // Debug log
            // Create an AudioSource to play the clip
            AudioSource.PlayClipAtPoint(SceneData.audioToPlay, Camera.main.transform.position);
            Debug.Log("Playing audio clip: " + SceneData.audioToPlay.name); // Debug log

            // Clear the audio clip so it doesn't replay if the scene reloads
            SceneData.audioToPlay = null;
        }
        else
        {
            Debug.Log("SceneData.audioToPlay is null."); // Debug log
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporterForPlayer : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private AudioClip audioToPlay; // Audio clip to play after scene loads

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider hit by: " + other.name); // Debug log

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected, loading scene: " + sceneToLoad); // Debug log

            if (audioToPlay != null)
            {
                Debug.Log("Assigning audio clip: " + audioToPlay.name); // Debug log
            }
            else
            {
                Debug.Log("No audio clip assigned."); // Debug log
            }

            // Store the audio clip in SceneData for access in the next scene
            SceneData.audioToPlay = audioToPlay;

            // Load the new scene
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.Log("Object not tagged as Player.");
        }
    }
}


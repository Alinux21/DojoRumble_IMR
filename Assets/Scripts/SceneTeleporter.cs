using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporter : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider hit by: " + other.name); // Debug log

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected, loading scene: " + sceneToLoad); // Debug log
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.Log("Object not tagged as Player.");
        }
    }

}
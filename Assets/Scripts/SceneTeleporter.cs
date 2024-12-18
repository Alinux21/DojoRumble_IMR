using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporter : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    private int hitCount = 0;
    public int maxHits = 5;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider hit by: " + other.name); // Debug log

        if (other.CompareTag("EnemySword"))
        {
            hitCount++;
            Debug.Log("Player detected, hit count: " + hitCount); // Debug log

            if (hitCount >= maxHits)
            {
                Debug.Log("Hit count reached 4, loading scene: " + sceneToLoad); // Debug log
                SceneManager.LoadScene(sceneToLoad);
            }
        }
        else
        {
            Debug.Log("Object not tagged as Player.");
        }
    }

}
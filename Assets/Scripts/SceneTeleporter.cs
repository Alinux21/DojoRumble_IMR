using UnityEngine;

public class SceneTeleporter : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FadeManager.Instance.FadeAndLoadScene(sceneToLoad);
        }
    }
}

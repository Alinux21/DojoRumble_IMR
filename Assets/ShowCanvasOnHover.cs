using UnityEngine;
using UnityEngine.UI; // Required for UI components


public class ShowCanvasOnHover : MonoBehaviour
{
    public GameObject targetCanvas;
    private void Start()
    {
        if (targetCanvas != null)
        {
            targetCanvas.SetActive(false);
        
        }
    }

    private void OnTriggerEnter(Collider other){
        
        if (other.CompareTag("Player"))
        {
            targetCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetCanvas.SetActive(false);
        }
    }

}

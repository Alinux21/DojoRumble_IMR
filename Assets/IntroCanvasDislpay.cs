using UnityEngine;
using UnityEngine.UI; // Required for UI components

public class ShowCanvasOnTrigger : MonoBehaviour
{
    public GameObject dojoCanvas;
    public GameObject winningCanvas;
    private void Start()
    {
        if (dojoCanvas != null && winningCanvas != null)
        {
            SetCorrectCanvas();
        }
    }

    private void SetCorrectCanvas(){
        if(GlobalManager.GetAllConditionsMet()==false){
            dojoCanvas.SetActive(true);
            winningCanvas.SetActive(false);
        }else{
            dojoCanvas.SetActive(false);
            winningCanvas.SetActive(true);
        }
    }

    private void UnsetCorrectCanvas(){
        dojoCanvas.SetActive(false);
        winningCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other){
        
        if (other.CompareTag("Player"))
        {
          SetCorrectCanvas();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnsetCorrectCanvas();
        }
    }

}

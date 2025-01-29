using UnityEngine;
using UnityEngine.UI; // Required for UI components

public class ShowCanvasOnTrigger : MonoBehaviour
{
    public GameObject dojoCanvas;
    public GameObject winningCanvas;
    public Button button;
    private void Start()
    {
        if (dojoCanvas != null && winningCanvas != null)
        {
            SetCorrectCanvas();
            button.onClick.AddListener(OnButtonClick);
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

    private void OnButtonClick()
    {
        dojoCanvas.SetActive(false);
        winningCanvas.SetActive(false);
    }
}

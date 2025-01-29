using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI components

public class HideCanvas : MonoBehaviour
{

    public Canvas uiCanvas;
    private bool canvasDisplayed=false;

    void OnTriggerExit(Collider other){

        if(canvasDisplayed==false){
        uiCanvas.gameObject.SetActive(false); 
        canvasDisplayed=true;
        
        if(GlobalManager.GetdrumVillage()==false){
            GlobalManager.SetdrumVillage(true);
        }
        
        }
        
    }

}

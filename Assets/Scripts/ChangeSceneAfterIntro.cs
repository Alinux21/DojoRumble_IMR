using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneAfterIntro : MonoBehaviour
{
    // This method will be called by the Signal Receiver
    public void ChangeScene()
    {
        Debug.Log("Changing scene to BattlefieldNight");
        SceneManager.LoadScene("BattlefieldNight");
    }
}

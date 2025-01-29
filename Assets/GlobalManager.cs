using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    public static bool teaRitual = false;
    public static bool drumVillage = false;
    public static bool samuraiTalks = false;

    // Optional: This flag can prevent multiple debug logs after all conditions are met
    private static bool allConditionsMet = false;

    void Update()
    {
        // Check if all conditions are met
        if (teaRitual && drumVillage && samuraiTalks && !allConditionsMet)
        {
            Debug.Log("All tasks completed! Debug log triggered!");
            allConditionsMet = true; // Prevents multiple logs
        }

        // if (allConditionsMet && SceneManager.GetActiveScene().name=="DojoAsset"){
        // }

    }

    public static bool GetAllConditionsMet(){
        return allConditionsMet;
    }

    // You can use these methods to set flags when specific conditions are met

    public static void SetteaRitual(bool value)
    {
        teaRitual = value;
        Debug.Log("Setting tea Ritual to true");

    }

    public static bool GetteaRitual()
    {
        return teaRitual;
    }

    public static void SetdrumVillage(bool value)
    {
        drumVillage = value;
        Debug.Log("Setting drum Village to true");

    }

    public static bool GetdrumVillage()
    {
        return drumVillage;
    }

    public static void SetsamuraiTalks(bool value)
    {
        samuraiTalks = value;
        Debug.Log("Setting samurai talks to true");
    }
    
    public static bool GetsamuraiTalks()
    {
        return samuraiTalks;
    }

    void Awake()
    {
        if (FindObjectsOfType<GlobalManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}

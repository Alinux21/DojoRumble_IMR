using UnityEngine;
using UnityEngine.Playables;

public class PlayTimelineOnStart : MonoBehaviour
{
    private PlayableDirector playableDirector;

    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        if (playableDirector != null)
        {
            playableDirector.Play();
        }
        else
        {
            Debug.LogError("PlayableDirector component not found on this GameObject.");
        }
    }
}


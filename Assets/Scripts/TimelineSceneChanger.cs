using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineSceneChanger : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public string sceneToLoad;

    void Start()
    {
        if (playableDirector != null)
        {
            playableDirector.stopped += OnTimelineStopped;
        }
    }

    void OnTimelineStopped(PlayableDirector director)
    {
        if (director == playableDirector)
        {
            FadeManager.Instance.FadeAndLoadScene(sceneToLoad);
        }
    }

    void OnDestroy()
    {
        if (playableDirector != null)
        {
            playableDirector.stopped -= OnTimelineStopped;
        }
    }
}
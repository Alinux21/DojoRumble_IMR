using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip[] dialogueClips; // Assign MP3s in Inspector
    [SerializeField] private AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !audioSource.isPlaying)
        {
            Debug.Log("Player entered trigger zone.");
            PlayRandomDialogue();
            if(GlobalManager.GetsamuraiTalks()==false){
                GlobalManager.SetsamuraiTalks(true);
            }
        }
        else
        {
            Debug.Log("Audio source is already playing.");
        }
    }

    private void PlayRandomDialogue()
    {
        if (dialogueClips.Length == 0) return;
        int clipIndex = Random.Range(0, dialogueClips.Length);
        audioSource.clip = dialogueClips[clipIndex];
        audioSource.Play();
    }
}
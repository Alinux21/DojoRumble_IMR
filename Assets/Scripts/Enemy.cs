using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public AudioClip hitSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void TakeDamage(int damage)
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
        Debug.Log("Enemy got hit!");
        GetComponent<EnemyAI>().OnHit(); // Notify AI of the hit
    }
}
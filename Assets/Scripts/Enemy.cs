using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy got hit!");
        GetComponent<EnemyAI>().OnHit(); // Notify AI of the hit
    }
}
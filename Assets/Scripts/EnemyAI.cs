using UnityEngine;
using UnityEngine.AI; // For navigation AI

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float attackRange = 2.5f;
    public float moveSpeed = 1.5f;
    public float attackCooldown = 0.5f;
    public int combatMode = 1;

    private Animation animationComponent;
    private NavMeshAgent navAgent;
    private float lastAttackTime = 0f;
    private AudioSource audioSource;
    private bool isInitialWaiting = true;
    private float waitEndTime;
    public AudioClip startAudioClip;
    public float initialWaitTime = 10f;

    void Start()
    {
        animationComponent = GetComponent<Animation>();
        navAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set up initial waiting period
        waitEndTime = Time.time + initialWaitTime;
        if (startAudioClip != null)
        {
            audioSource.clip = startAudioClip;
            audioSource.Play();
        }

        if (animationComponent == null)
        {
            Debug.LogError("No Animation component found on this GameObject. Add an Animation component and assign animations.");
            return;
        }

        if (!animationComponent["Walk"] || !animationComponent["Attack"] || !animationComponent["idle"])
        {
            Debug.LogError("Animations 'Walk', 'Attack', and 'idle' are missing. Ensure they are assigned in the Animation component.");
        }
    }

    void Update()
    {
        // Check if still in initial waiting period
        if (isInitialWaiting)
        {
            if (Time.time < waitEndTime)
            {
                navAgent.isStopped = true;
                if (!animationComponent.IsPlaying("idle"))
                {
                    animationComponent.CrossFade("idle");
                }
                return;
            }
            isInitialWaiting = false;
        }

        if (combatMode == 0)
        {
            // Stop movement and play idle animation when inactive
            navAgent.isStopped = true;
            if (!animationComponent.IsPlaying("idle"))
            {
                animationComponent.CrossFade("idle");
            }
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Rotate to face the player
        Vector3 lookDirection = player.position - transform.position;
        lookDirection.y = 0; // Keep the rotation horizontal
        transform.rotation = Quaternion.LookRotation(lookDirection);

        if (distanceToPlayer > attackRange)
        {
            ChasePlayer();
        }
        else
        {
            HandleAttack();
        }
    }

    void ChasePlayer()
    {
        navAgent.isStopped = false;
        navAgent.SetDestination(player.position);

        if (!animationComponent.IsPlaying("Walk"))
        {
            animationComponent.CrossFade("Walk");
        }
    }

    void HandleAttack()
    {
        navAgent.isStopped = true;

        // If cooldown is active, play "idle"
        if (Time.time < lastAttackTime + attackCooldown && Time.time > lastAttackTime)
        {
            if (!animationComponent.IsPlaying("idle"))
            {
                animationComponent.CrossFade("idle");
            }
            return;
        }
        if (Time.time < lastAttackTime + attackCooldown)
        {
            return;
        }
        
        AttackPlayer();
    }

    void AttackPlayer()
    {
        lastAttackTime = Time.time + animationComponent["Attack"].length;
        animationComponent.CrossFade("Attack");
    }

    public void OnHit()
    {
        // animationComponent.CrossFade("idle");
        // Vector3 stepBackDirection = transform.position - player.position;
        // stepBackDirection.Normalize();
        // transform.position += stepBackDirection * 2f;
    }
}
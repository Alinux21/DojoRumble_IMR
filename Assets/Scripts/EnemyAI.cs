using UnityEngine;
using UnityEngine.UI; // For UI components like Button
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
    private bool waitingForStart = true; // Wait for button press
    private float waitEndTime;
    public AudioClip startAudioClip;
    public float initialWaitTime = 10f;

    public Button startButton; // UI Button for starting the AI
    public Canvas uiCanvas;    // Canvas to hide when the button is pressed

    void Start()
    {
        animationComponent = GetComponent<Animation>();
        navAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
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

        // Assign button's onClick event if startButton is set in the Inspector
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartEnemyAI);
        }

        navAgent.isStopped = true; // Ensure the enemy doesn't move while waiting
    }

    void Update()
    {
        // Wait for a button press to start
        if (waitingForStart)
        {
            if (!animationComponent.IsPlaying("idle"))
            {
                animationComponent.CrossFade("idle");
            }
            return;
        }

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

    public void StartEnemyAI() // Called by the button press
    {
        waitingForStart = false; // Mark that the start button was pressed
        waitEndTime = Time.time + initialWaitTime; // Set the initial waiting period

        if (startAudioClip != null)
        {
            audioSource.clip = startAudioClip;
            audioSource.Play();
        }

        // Hide the UI Canvas
        if (uiCanvas != null)
        {
            uiCanvas.gameObject.SetActive(false);
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

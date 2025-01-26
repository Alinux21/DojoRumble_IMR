using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{
    public Camera player;
    public string nextScene;
    public Animation animationComponent;
    public float bowDistance = 4f;
    public float bowHeightThreshold = 0.2f; // How much the player needs to lower their head
    public float waitBeforeBow = 1f;
    
    private float initialCameraHeight;
    private bool hasBowed = false;
    private bool hasSetInitialHeight = false;

    void Start()
    {
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        float currentHeight = player.transform.position.y;

        // Set initial height when first getting close
        if (distance < bowDistance && !hasSetInitialHeight)
        {
            initialCameraHeight = currentHeight;
            hasSetInitialHeight = true;
        }

        // Only check for bowing if we have set initial height
        if (hasSetInitialHeight)
        {
            float heightDifference = initialCameraHeight - currentHeight;

            if (distance < bowDistance && heightDifference > bowHeightThreshold && !hasBowed)
            {
                if (!animationComponent.IsPlaying("bow"))
                {
                    hasBowed = true;
                    StartCoroutine(DelayedBow());
                }
            }
        }
        
        // Reset both flags when moving away
        if (distance > bowDistance)
        {
            hasBowed = false;
            hasSetInitialHeight = false;
        }
    }

    IEnumerator DelayedBow()
    {
        yield return new WaitForSeconds(waitBeforeBow); // Wait for 1 second
        animationComponent.Play("bow");
        StartCoroutine(ReturnToIdle());
    }

    IEnumerator ReturnToIdle()
    {
        // Wait for bow animation to finish
        yield return new WaitForSeconds(animationComponent["bow"].length);
        animationComponent.Play("idle");
        
        // Wait additional time before scene transition
        yield return new WaitForSeconds(1.5f);
        FadeManager.Instance.FadeAndLoadScene(nextScene);
    }
}

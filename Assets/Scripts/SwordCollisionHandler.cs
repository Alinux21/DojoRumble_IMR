using UnityEngine;

public class SwordCollisionHandler : MonoBehaviour
{
    public GameObject bloodEffect;
    public GameObject sparkEffect;

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        HandleCollision(collision.gameObject, contact.point);
    }

    void OnTriggerEnter(Collider other)
    {
        Vector3 hitPosition = transform.position;
        HandleCollision(other.gameObject, hitPosition);
    }

    void HandleCollision(GameObject collidedObject, Vector3 hitPosition)
    {
        int layer = collidedObject.layer;
        if (layer == LayerMask.NameToLayer("EnemyBody"))
        {
            GameObject effect = Instantiate(bloodEffect, hitPosition, Quaternion.identity);
            Destroy(effect, 1f); // Destroy after 1 second
            Debug.Log("Hit enemy body");
        }
        else if (layer == LayerMask.NameToLayer("EnemySword"))
        {
            GameObject effect = Instantiate(sparkEffect, hitPosition, Quaternion.identity);
            Destroy(effect, 1f); // Destroy after 1 second
            Debug.Log("Hit enemy sword");
        }
    }
}
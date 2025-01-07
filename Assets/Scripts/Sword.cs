using UnityEngine;

public class Sword : MonoBehaviour
{
    public int damage = 25;
    public GameObject bloodEffect;
    public GameObject sparkEffect;

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);

            InstantiateEffect(bloodEffect, other.transform.position);
        }

        if (other.CompareTag("Sword"))
        {
            InstantiateEffect(sparkEffect, transform.position);
        }
    }

    private void InstantiateEffect(GameObject effect, Vector3 position)
    {
        if (effect != null)
        {
            GameObject instantiatedEffect = Instantiate(effect, position, Quaternion.identity);
            Destroy(instantiatedEffect, 1f); // Destroy after 1 second
        }
    }
}
using UnityEngine;
using System.Collections;
using System.Linq.Expressions;

public class ExplodingObject : MonoBehaviour, IBulletHitListener
{
    public GameObject explosionPrefab;
    public int strength = 1;
    public float explosionScale = 0.5f;

    [Header("Damage")]
    public float explosionDamage = 100f;
    public float explosionRange = 5f;

    public void OnBulletHit(BulletHitInfo hit)
    {
        strength--;

        if (strength == 0)
        {
            var go = GameObject.Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            go.transform.localScale = Vector3.one * explosionScale;

            Collider[] explosionHits = Physics.OverlapSphere(transform.position, explosionRange);

            foreach (Collider explosionHit in explosionHits)
            {
                IExplosionHitListener explosionHitListener = explosionHit.GetComponent<IExplosionHitListener>();

                float distance = Vector2.Distance(new Vector2(explosionHit.transform.position.x, explosionHit.transform.position.z),
                    new Vector2(transform.position.x, transform.position.z));

                explosionHitListener.OnExplosionHit(explosionDamage * distance / explosionRange);
            }

            GameObject.Destroy(this.gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}

using UnityEngine;
using System.Collections;

public class ExplodingObject : MonoBehaviour, IBulletHitListener
{
    public int strength = 1;
    public GameObject explosionPrefab;
    public float explosionScale = 0.5f;

    public void OnBulletHit(BulletHitInfo hit)
    {
        strength--;

        if (strength == 0)
        {
            var go = GameObject.Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            go.transform.localScale = Vector3.one * explosionScale;

            GameObject.Destroy(this.gameObject);
        }
    }
}

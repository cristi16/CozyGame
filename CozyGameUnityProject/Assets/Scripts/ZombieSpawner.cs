using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public const int k_maxNumberOfZombies = 300;
    public static int zombieCount = 0;

    public GameObject zombiePrefab;
    public int spawnCount = 5;
    public int spawnInterval = 3;
    public float spawnRangeDistance = 5f;

    IEnumerator Start()
    {
        if(zombiePrefab == null) yield break;

        while (true)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                if (zombieCount < k_maxNumberOfZombies)
                {
                    Vector2 random = Random.insideUnitCircle * spawnRangeDistance;
                    GameObject.Instantiate(zombiePrefab, transform.position + new Vector3(random.x, 0f, random.y), Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

}

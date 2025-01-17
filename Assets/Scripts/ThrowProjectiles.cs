using UnityEngine;
using System.Collections;

public class ThrowProjectiles : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float projectileSpeed = 10f;

    private Vector3 spawnPos;

    void Update()
    {
        spawnPos = transform.position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject instantiatedProjectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
            instantiatedProjectile.transform.parent = gameObject.transform;

            StartCoroutine(MoveProjectile(instantiatedProjectile));
        }

    }

    private IEnumerator MoveProjectile(GameObject projectile)
    {
        while (projectile != null)
        {
            // TODO : fix parent orientation to use forward instead of up
            projectile.transform.position += transform.up * projectileSpeed * Time.deltaTime;
            yield return null;
        }
    }
}

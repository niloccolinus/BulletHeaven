using UnityEngine;
using System.Collections;

public class ThrowProjectiles : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float projectileSpeed = 10f;
    [SerializeField]
    private float xpCost = 10f;

    private Vector3 spawnPos;

    void Update()
    {
        spawnPos = transform.position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // check if player has enough xp
            if (GameManager.Instance.PlayerXP >= xpCost)
            {
                // reduce xp
                GameManager.Instance.PlayerXP -= xpCost;

                // shoot projectile
                GameObject instantiatedProjectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
                instantiatedProjectile.transform.parent = gameObject.transform;

                // play firing sound
                SoundManager.PlaySound(SoundType.FIRE);

                StartCoroutine(MoveProjectile(instantiatedProjectile));
            }
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

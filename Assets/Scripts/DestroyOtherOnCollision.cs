using UnityEngine;

public class DestroyOtherOnCollision : MonoBehaviour
{
    [SerializeField]
    private bool destroyItself;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            if (destroyItself) Destroy(gameObject);
        }

    }
}

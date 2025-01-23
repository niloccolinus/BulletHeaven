using UnityEngine;

public class DestroyOtherOnCollision : MonoBehaviour
{
    [SerializeField]
    private bool _destroyItself = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            if (_destroyItself) Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private float _value = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PlayerXP += _value;

                // Play collect sound
                SoundManager.PlaySound(SoundType.COLLECT);
            }

            Destroy(gameObject); // destroy collectable once collected
        }
    }
}

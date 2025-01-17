using UnityEngine;

public class SatelliteController : MonoBehaviour
{
    [SerializeField] 
    private float rotationSpeed = 100f;
    [SerializeField]
    private Transform player;

    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        transform.position = player.position;
    }
}

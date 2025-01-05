using UnityEngine;

public class SatelliteRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;

    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}

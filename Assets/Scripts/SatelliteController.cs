using UnityEngine;

public class SatelliteController : MonoBehaviour
{
    [SerializeField] 
    private float _rotationSpeed = 100f;
    [SerializeField]
    private Transform _player;

    private void Update()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
        transform.position = _player.position;
    }
}

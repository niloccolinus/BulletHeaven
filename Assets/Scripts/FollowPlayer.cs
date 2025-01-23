using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] 
    private Transform _player;

    void Update()
    {
        transform.position = _player.transform.position + new Vector3(0, 6, -16);
    }
}
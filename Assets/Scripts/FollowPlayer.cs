using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] 
    private Transform player;

    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 6, -16);
    }
}
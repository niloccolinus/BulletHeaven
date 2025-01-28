using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _laser;
    [SerializeField]
    private float _xpCost = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // check if player has enough xp
            if (GameManager.Instance.PlayerXP >= _xpCost)
            {
                // reduce xp
                GameManager.Instance.PlayerXP -= _xpCost;

                // shoot projectile
                _laser.enabled = true;
            }   
        }
    }
}

using UnityEngine;

public class Scoring : MonoBehaviour
{
    [SerializeField]
    private int _objectScoring;
    [SerializeField]
    private GameObject _xpCollectablePrefab;

    public void GrantRewards()
    {
        GameManager.Instance.TotalScore += _objectScoring;
        Instantiate(_xpCollectablePrefab, transform.position, Quaternion.identity);
    }
}


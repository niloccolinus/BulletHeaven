using UnityEngine;

public class SatelliteManager : MonoBehaviour
{
    [SerializeField]
    private int _satelliteCount = 3;
    public int SatelliteCount 
    {
        get => _satelliteCount; 
        private set => _satelliteCount = Mathf.Clamp(value, 0, _maxSatellites);
    }

    [SerializeField] 
    private GameObject _satellitePrefab;
    [SerializeField] 
    private Transform _satellitesParent;
    [SerializeField]
    [Range(0, 10)] 
    private int _maxSatellites = 10;
    [SerializeField]
    [Range(0.2f, 10.0f)] 
    private float _orbitRadius = 2.0f;

    private GameObject[] _satellites;

    private void Start()
    {
        UpdateSatellites();
    }

    // Cette méthode est appelée lorsque des valeurs changent dans l'inspecteur
    private void OnValidate()
    {
        // Clamp le nombre de satellites pour rester dans les limites autorisées
        _satelliteCount = Mathf.Clamp(_satelliteCount, 0, _maxSatellites);

        // Vérifie si l'application est en mode lecture pour éviter les erreurs
        if (Application.isPlaying)
        {
            UpdateSatellites();
        }
    }

    public void UpdateSatellites()
    {
        // Détruire les anciens satellites s'ils existent
        if (_satellites != null)
        {
            foreach (var satellite in _satellites)
            {
                if (satellite != null)
                {
                    Destroy(satellite);
                }
            }
        }

        // Initialiser un nouveau tableau pour les satellites
        _satellites = new GameObject[_satelliteCount];

        // Placer les satellites de manière régulière autour du robot
        for (int i = 0; i < _satelliteCount; i++)
        {
            // Calcul de l'angle pour chaque satellite
            float angle = i * 360f / _satelliteCount;
            Vector3 position = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad) * _orbitRadius,
                0,
                Mathf.Sin(angle * Mathf.Deg2Rad) * _orbitRadius
            );

            // Instancier le satellite et le positionner
            GameObject newSatellite = Instantiate(_satellitePrefab, _satellitesParent);
            newSatellite.transform.localPosition = position;

            // Ajouter la rotation manuelle en X et en Y
            Quaternion baseRotation = Quaternion.Euler(90, 90, 0); // Rotation fixe pour aligner correctement
            Quaternion angleRotation = Quaternion.Euler(0, -angle, 0); // Rotation autour du cercle
            newSatellite.transform.localRotation = angleRotation * baseRotation; // Combinaison des deux rotations

            // Ajouter au tableau des satellites
            _satellites[i] = newSatellite;
        }
    }
}

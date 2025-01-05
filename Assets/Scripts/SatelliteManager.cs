using UnityEngine;

public class SatelliteManager : MonoBehaviour
{
    public int satelliteCount = 3;
    [SerializeField] private GameObject satellitePrefab;
    [SerializeField] private Transform satellitesParent;
    [SerializeField][Range(0, 10)] private int maxSatellites = 10;
    [SerializeField][Range(0.2f, 10.0f)] private float orbitRadius = 2.0f; // Plage d�finie directement avec [Range]

    private GameObject[] satellites;

    private void Start()
    {
        UpdateSatellites();
    }

    // Cette m�thode est appel�e lorsque des valeurs changent dans l'inspecteur
    private void OnValidate()
    {
        // Clamp le nombre de satellites pour rester dans les limites autoris�es
        satelliteCount = Mathf.Clamp(satelliteCount, 0, maxSatellites);

        // V�rifie si l'application est en mode lecture pour �viter les erreurs
        if (Application.isPlaying)
        {
            UpdateSatellites();
        }
    }

    public void UpdateSatellites()
    {
        // D�truire les anciens satellites s'ils existent
        if (satellites != null)
        {
            foreach (var satellite in satellites)
            {
                if (satellite != null)
                {
                    Destroy(satellite);
                }
            }
        }

        // Initialiser un nouveau tableau pour les satellites
        satellites = new GameObject[satelliteCount];

        // Placer les satellites de mani�re r�guli�re autour du robot
        for (int i = 0; i < satelliteCount; i++)
        {
            // Calcul de l'angle pour chaque satellite
            float angle = i * 360f / satelliteCount;
            Vector3 position = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad) * orbitRadius,
                0,
                Mathf.Sin(angle * Mathf.Deg2Rad) * orbitRadius
            );

            // Instancier le satellite et le positionner
            GameObject newSatellite = Instantiate(satellitePrefab, satellitesParent);
            newSatellite.transform.localPosition = position;

            // Ajouter la rotation manuelle en X et en Y
            Quaternion baseRotation = Quaternion.Euler(90, 90, 0); // Rotation fixe pour aligner correctement
            Quaternion angleRotation = Quaternion.Euler(0, -angle, 0); // Rotation autour du cercle
            newSatellite.transform.localRotation = angleRotation * baseRotation; // Combinaison des deux rotations

            // Ajouter au tableau des satellites
            satellites[i] = newSatellite;
        }
    }
}

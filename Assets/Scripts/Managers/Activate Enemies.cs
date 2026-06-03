using System.Runtime.CompilerServices;
using UnityEngine;

public class ActivateEnemies : MonoBehaviour
{
    [SerializeField] GameObject[] gates;

    const string PLAYER_STRING = "Player";

    // collider volume for the "tutorial" that activates enemies in the scene

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;

        if (other.CompareTag(PLAYER_STRING))
        {
            foreach (var gate in gates)
            {
                gate.gameObject.SetActive(true);
            }
        }
        Destroy(gameObject);
    }
}

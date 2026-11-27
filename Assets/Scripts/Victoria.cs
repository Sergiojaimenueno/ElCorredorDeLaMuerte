using UnityEngine;
using UnityEngine.SceneManagement;

public class Victoria : MonoBehaviour
{
    public string escenaFinal = "Victoria";

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            SceneManager.LoadScene(escenaFinal);
        }
    }
}
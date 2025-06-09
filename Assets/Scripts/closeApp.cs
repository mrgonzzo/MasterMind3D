using UnityEngine;
using UnityEngine.SceneManagement;

public class AppController : MonoBehaviour
{
    public void Salir()
    {
        Application.Quit();
        Debug.Log("La aplicación se está cerrando...");
    }

    public void NuevaPartida()
    {
        // Recarga la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

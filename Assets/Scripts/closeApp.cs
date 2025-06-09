using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void Salir()
    {
        Application.Quit();
        Debug.Log("La aplicación se está cerrando...");
    }
}

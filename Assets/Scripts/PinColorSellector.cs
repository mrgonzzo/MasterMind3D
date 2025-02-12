using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinColorSellector : MonoBehaviour
{
    public string numeroStr;
    // Este método lo llamaremos desde el GameController
    public void ColorSelect()
    {
        // Asegúrate de que el objeto tenga el tag "pin"
        if (gameObject.CompareTag("pin"))
        {
            // Obtener el nombre del objeto
            string nombre = gameObject.name;

            // Intentar extraer el número de la forma "Color_n"
          //  int numero = 0;
            if (nombre.Contains("Color_"))
            {
                // Tomar el número después de "Color_"
                numeroStr = nombre.Substring(6); // Esto obtiene la parte después de "Color_"
                /*if (int.TryParse(numeroStr, out numero))
                {
                    // Aquí puedes hacer algo con el número, como almacenarlo o usarlo
                    Debug.Log("PinColorSellector Has cogido el color número: " + numero);
                }
                else
                {
                    Debug.LogError("El nombre no tiene un número válido.");
                }*/
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinColorSellector : MonoBehaviour
{
    public string numeroStr;
    // Este m�todo lo llamaremos desde el GameController
    public void ColorSelect()
    {
        // Aseg�rate de que el objeto tenga el tag "pin"
        if (gameObject.CompareTag("pin"))
        {
            // Obtener el nombre del objeto
            string nombre = gameObject.name;

            // Intentar extraer el n�mero de la forma "Color_n"
          //  int numero = 0;
            if (nombre.Contains("Color_"))
            {
                // Tomar el n�mero despu�s de "Color_"
                numeroStr = nombre.Substring(6); // Esto obtiene la parte despu�s de "Color_"
            }
        }
    }
}

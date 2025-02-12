using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{   
    public int[] secretCodeArray = new int[4];
    //public int pinseleccionado = 18;
    int colorNum;
    SecretCodeManager secretCodeManager = new SecretCodeManager();
    // Asignar manualmente las esferas desde el Inspector
    public GameObject[] esferas;
    public string numeroColor;
    public PinColorPainter pinColorPainter;  // Referencia a PinColorPainter para pintar los cubos
    GameObject clickedObject;

    TurnController tc;
    

    private void Awake()
    {
        GameObject go_tc = new GameObject("TurnController");
        tc = go_tc.AddComponent<TurnController>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        // creamos y tapamos el codigo secreto
        secretCodeArray = secretCodeManager.CodeGenerator(4, 1, 6);
        secretCodeManager.CoverSwitch();
        secretCodeManager.DrawSecretCode(secretCodeArray);
        // habilitamos el boxcollider del turno_0
        tc.EnableTurn("Code_0");
    }

    // Update is called once per frame
    //[Obsolete]
    void Update()
    {
        // Verificamos si se hizo clic con el botón izquierdo del ratón
        if (Input.GetMouseButtonDown(0)) // Al hacer clic izquierdo
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Comprobamos si el rayo golpeó algo
            if (Physics.Raycast(ray, out hit))
            {
                // Comprobamos si el objeto tocado es una de las esferas en nuestro array
                clickedObject = hit.collider.gameObject;

                foreach (var esfera in esferas)
                {
                    if (clickedObject == esfera)
                    {
                        // Llamamos al método ColorSelect desde el script PinColorSellector
                        PinColorSellector PinColorSellectorScript = esfera.GetComponent<PinColorSellector>();
                        if (PinColorSellectorScript != null)
                        {
                            PinColorSellectorScript.ColorSelect();
                            numeroColor = PinColorSellectorScript.numeroStr;  // Guardamos el número del color
                            pinColorPainter.colorNum = numeroColor;
                        }
                    } 
                } // for each
                  //fuera de for each pero dentro Physics.Raycast(ray, out hit) que detecta la colision
                  // Si el objeto golpeado es un cubo
                if (clickedObject.name.StartsWith("C_"))  // Validar que sea un cubo
                {
                   // Debug.Log("GameController Cubo seleccionado: " + clickedObject.name);
                    pinColorPainter.PintarCubos(clickedObject);
                }

            }
        }
    }


}





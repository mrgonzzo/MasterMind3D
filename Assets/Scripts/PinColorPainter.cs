using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.VisualScripting.StickyNote;
using static UnityEditor.PlayerSettings;
using Color = UnityEngine.Color;

public class PinColorPainter : MonoBehaviour
{
    //private GameController controller;
    //public GameObject[] cubosCodigo; // Los cubos dentro de la apuesta
    public GameObject cuboPCP;
    public string colorNum;
    //string colorNom;
    Color color;
    public int posicionCubo;//esto nos dira la posicion del dato en el array (betArray) a comparar con el secreto 
    public int[] betArray = new int[4];

    // Este método asigna el color al cubo "posicion" de la apuesta
    public void PintarCubos(GameObject cubo)
    {
        cuboPCP = cubo;
        // Obtenemos el color seleccionado desde GameController
        if (!string.IsNullOrEmpty(colorNum))
        {
           // int colorNum = int.Parse(controller.numeroColor);  // Convertimos el número de color de string a int
            //Debug.Log("PinColorPainter  controller.numeroColor: " + colorNum);
            // Asignar el color basado en el número seleccionado
            switch (colorNum)
            {
                case "1":
                    color = Constantes.verde;
                    break;
                case "2":
                    color = Constantes.azul;
                    break;
                case "3":
                    color = Constantes.rojo;
                    break;
                case "4":
                    color = Constantes.amarillo;
                    break;
                case "5":
                    color = Constantes.morado;
                    break;
                case "6":
                    color = Constantes.naranja;
                    break;
                default:
                    color = Constantes.grisActivo;  // Si el número no coincide, asignamos gris
                    break;
            }

            //capturo el nombre del objeto que he pinchado para montar el array apuesta
            string nombre = cubo.name;
            // Cambio el color de cada cubo
            cubo.GetComponent<Renderer>().material.color = color;  
            //doy valor a la posicion correspondiente del betarray 
              //extraigo la posicion i del nombre C_i
            string StrPosicionCubo = nombre.Substring(2);
              // convierto la cadena "i" extraida del nombre y la convierto al entero i
            int posicionCubo = int.Parse(StrPosicionCubo);
            betArray[posicionCubo] = int.Parse(colorNum);
            //Debug.Log("PinColorPainter   posicionCubo = " + posicionCubo + "vale " + betArray[posicionCubo]);
        }
        else
        {
            Debug.LogError("PinColorPainter No se ha seleccionado un color en GameController.");
        }
    }
    public void pintarRespuesta(GameObject cubo, Color color)
    {
        cubo.GetComponent<Renderer>().material.color = color;
    }
}
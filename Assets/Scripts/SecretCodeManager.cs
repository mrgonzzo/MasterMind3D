using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretCodeManager
{

    public int[] CodeGenerator(int length, int minValue, int maxValue)
    {
        int[] array = new int[length];
        for (int i = 0; i < length; i++)
        {
            array[i] = UnityEngine.Random.Range(minValue, maxValue + 1);
        }
        return array;
    }

   public void DrawSecretCode(int[] secretCodeArray)
    {
        // Busca el objeto hijo "SC_0" dentro de "SecretCode"
        Transform secretCode = GameObject.Find("SecretCode").transform;
        Debug.Log("SecretCodeManager secretCodeArray[] = [" + secretCodeArray[0] + "," + secretCodeArray[1] + "," + secretCodeArray[2] + "," + secretCodeArray[3] + "]");
        for (int i = 0; i <= 3; i++)
        {
            // buscamos el hijo de secretCode correspondiente
            Transform sc = secretCode.Find("SC_" + i);
            // y accedemos a su render
            Renderer renderer = sc.GetComponent<Renderer>();
            //traducimos los numero a nuestros colores y pintamos
            switch (secretCodeArray[i])
            {
                case 1: //Verde
                    renderer.material.color = Constantes.verde;
                    break;
                case 2: //Azul
                    renderer.material.color = Constantes.azul;
                    break;
                case 3: //Rojo
                    renderer.material.color = Constantes.rojo;
                    break;
                case 4: //Amarillo
                    renderer.material.color = Constantes.amarillo;
                    break;
                case 5: // morado
                    renderer.material.color = Constantes.morado;
                    break;
                case 6: //Naranja
                    renderer.material.color = Constantes.naranja;
                    break;
            } //switch
        } //for
    } //metodo*/

    
    public void CoverSwitch()
    {
        GameObject coverObject = GameObject.Find("Cover");
        if (coverObject == null)
        {
            Debug.Log("SecretCodeManager error al asignar la tapa");
        }
        else
        {
            // Verifica si el objeto tiene un MeshRenderer
            MeshRenderer meshRenderer = coverObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                // Desactiva el MeshRenderer (no se "pintará" el objeto)
                meshRenderer.enabled = !meshRenderer.enabled;

            }
        }
    }


}
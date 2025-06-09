using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class TurnController : MonoBehaviour
{

    PinColorPainter pcp;
    GameController gc;
    GameObject Cover;
    int[] beta;
    int[] sca;
    int turno;

    private void Start()
    {
        turno = 0;        
        gc = FindFirstObjectByType<GameController>();// FindObjectOfType<GameController>(); // Busca el GameController en la escena
        Cover = GameObject.FindWithTag("cover"); 
        
    }

    public void PlayTurn()
       {
        pcp = FindFirstObjectByType<PinColorPainter>();
        
        // 1 poner el boxcollider del turno code_turno a off
        DisableTurn("Code_" + turno);
   
        // 2 Comprobamos que los arrays GameController.secretCodeArray vs. PinColorPainter.betArray  llegan correctamente

        sca = gc.secretCodeArray;

        string nombre = gameObject.name;

        if (gc.secretCodeArray == null)
        {
            Debug.Log("TurnController  NO hay secretCodeArray en tc");
        }
        else
        {
            if (gc.secretCodeArray.Length == 4)
            {
                sca = gc.secretCodeArray;
            }
            else 
            { 
                Debug.Log("TurnController algo pasa con secretCodeArray: Longitud = " + sca.Length); 
            }
        }
        
        
        if (pcp.betArray == null)
        {
            Debug.Log("TurnController  NO hay pcp.betArray en tc");
        }
        else
        {
            if (pcp.betArray.Length == 4)
            {
                beta = pcp.betArray;
                Debug.Log("SecretCodeManager betArray[] = [" + beta[0] + "," + beta[1] + "," + beta[2] + "," + beta[3] + "]");

            }
            else
            {
                Debug.Log("TurnControlleralgo pasa con : betArray" + beta.Length);
            }

        }
        //3 los comparamos
        int pines = generarRespuesta(sca, beta);
       // 4a fin partida si secretCodeArray = betArray
       // 4b avanzar el turno si secretCodeArray <> betArray  
       if (pines == 4 || turno == 9)
        {
            // fin del juego 

            Cover.GetComponent<MeshRenderer>().enabled = false;

            Debug.Log("TurnController FIN DEL JUEGO:   pines =" +  pines + " turno = " + turno);
           
        }
        else 
        {
            Debug.Log("TurnController pines != 4 CONTINUAMOS PARA BINGO   pines =" + pines);
            turno++;
            EnableTurn("Code_" + turno);
          //  Debug.Log("TurnController turno " + turno + " enabled");
        }
       
    }

    //ChildCollidersByName
    public void DisableTurn(string parentObjectName)
    {
        // Localiza el objeto por su nombre
        GameObject parentObject = GameObject.Find(parentObjectName);

        if (parentObject != null)
        {
            // Encuentra todos los hijos del objeto
            Transform[] childTransforms = parentObject.GetComponentsInChildren<Transform>();

            foreach (Transform child in childTransforms)
            {
                // Omite el propio objeto padre
                if (child != parentObject.transform)
                {
                    // Intenta obtener el Collider y desactivarlo
                    Collider childCollider = child.GetComponent<Collider>();
                    if (childCollider != null)
                    {
                        childCollider.enabled = false;
                    }
                }
            }
        }
        else
        {
            Debug.LogError("TurnController DisableTurn No se encontró un objeto con el nombre especificado.");
        }
    }
    public void EnableTurn(string parentObjectName)
    {
        // Localiza el objeto por su nombre
        GameObject parentObject = GameObject.Find(parentObjectName);

        if (parentObject != null)
        {
            // Encuentra todos los hijos del objeto
            Transform[] childTransforms = parentObject.GetComponentsInChildren<Transform>();

            foreach (Transform child in childTransforms)
            {
                // Omite el propio objeto padre
                if (child != parentObject.transform)
                {
                    // Intenta obtener el Collider y desactivarlo
                    Collider childCollider = child.GetComponent<Collider>();
                    if (childCollider != null)
                    {
                        childCollider.enabled = true;
                    }
                }
            }
        }
        else
        {
            Debug.LogError("TurnController EnableTurn No se encontró un objeto con el nombre especificado.");
        }
    }
    // metodo generarRespuesta() generado por IA
    public int generarRespuesta(int[] secret, int[] bet)
    {
        UnityEngine.Color color;
        int pinesNegros = 0;
        GameObject respuesta = GameObject.Find("Response_" + turno); // Mejor: guardar referencia antes
        bool[] secretUsado = new bool[secret.Length];
        bool[] betUsado = new bool[bet.Length];

        // Primero, pines negros (aciertos exactos)
        for (int i = 0; i < bet.Length; i++)
        {
            if (bet[i] == secret[i])
            {
                Transform hijoTransform = respuesta.transform.Find("R_" + i);
                GameObject cubo = hijoTransform.gameObject;
                color = Constantes.negro;
                pcp.pintarRespuesta(cubo, color);
                pinesNegros++;
                secretUsado[i] = true;
                betUsado[i] = true;
            }
        }

        // Luego, pines blancos (aciertos de color en posición incorrecta)
        for (int i = 0; i < bet.Length; i++)
        {
            if (betUsado[i]) continue;
            for (int j = 0; j < secret.Length; j++)
            {
                if (!secretUsado[j] && bet[i] == secret[j])
                {
                    Transform hijoTransform = respuesta.transform.Find("R_" + i);
                    GameObject cubo = hijoTransform.gameObject;
                    color = Constantes.blanco;
                    pcp.pintarRespuesta(cubo, color);
                    secretUsado[j] = true;
                    break;
                }
            }
        }

        return pinesNegros;
    }

    /* public int generarRespuesta(int[] secret, int[] bet)
     {//PintarCubos(GameObject cubo)
         UnityEngine.Color color = new UnityEngine.Color();
         int pinesNegros = 0;
         GameObject respuesta = GameObject.Find("Response_" + turno);
         //Debug.Log("TurnController generarRespuesta en " + respuesta.name);
         for (int i = 0; i < bet.Length; i++)
         {
             Debug.Log("TurnController generarRespuesta i = " + i + " bet[i] = " + bet[i]);
             Transform hijoTransform = respuesta.transform.Find("R_" + i);
             GameObject cubo = hijoTransform.gameObject;
              for (int j = 0; j < secret.Length; j++)
             {
                 Debug.Log("TurnController generarRespuesta  j = " + j + " secret[j] = " + secret[j]);
                 if (secret[j] == bet[i])
                 {
                     if (i == j)
                     {
                         Debug.Log("TurnController generarRespuesta pinto la respuesta " + cubo.name + " de NEGRO i == j");
                         Debug.Log("TurnController generarRespuesta i = " + i + " bet[i] = " + bet[i] + " j = " + j + " secret[j] = " + secret[j]);
                         color = Constantes.negro;
                         pcp.pintarRespuesta(cubo,color);
                         pinesNegros++;
                     }
                     else
                     {
                         Debug.Log("TurnController generarRespuesta pinto la respuesta " + cubo.name + "  de BLANCO i != j");
                         Debug.Log("TurnController generarRespuesta i = " + i + " bet[i] = " + bet[i] + " j = " + j + " secret[j] = " + secret[j]);
                         color = Constantes.blanco;
                         pcp.pintarRespuesta(cubo,color);                
                     }

                 }
             }
         }
         return pinesNegros;
     }*/
}
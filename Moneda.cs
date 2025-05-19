using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
   [SerializeField] private AudioClip colectar1;
   public GameObject efecto;
   public float Puntos;
   public Puntaje Puntaje;

   
   private void OnTriggerEnter2D(Collider2D colision)
   {
        if(colision.CompareTag("Player"))
        {
            ControladorDeSonido.Instance.EjecutarSonido(colectar1);
            Puntaje.SumPuntos(Puntos);
            CtrlPuntos.Instance.SumPuntos(Puntos);
            Instantiate(efecto, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
   }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlPuntos : MonoBehaviour
{
   public static CtrlPuntos Instance; 
   public float cantidadPuntos;

   public void Awake()
   {
        if(CtrlPuntos.Instance == null)
        {
            CtrlPuntos.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
   }
    public void SumPuntos(float Puntos)
    {
        cantidadPuntos += Puntos;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuntoControl : MonoBehaviour
{
  private Animator animator;
  ControladorPlayer controladorPlayer;
  public int PrecioCheckpoint ;
  Puntaje puntaje;
  private bool puntosRestados = false;

  [SerializeField] private GameObject Panelfalta;
  
   private void Start()
   {
     animator = GetComponent<Animator>();
   }

   private void Awake()
   {
    controladorPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<ControladorPlayer>();
    puntaje = GameObject.FindObjectOfType<Puntaje>();
    
    
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
    
    
    if (other.CompareTag("Player"))
    {
      if (!puntosRestados)
      {
          if (puntaje.cantidadPuntos >= PrecioCheckpoint)
          {
            animator.SetTrigger("Activar");
            controladorPlayer.CargarCheckpoint(transform.position);
            puntaje.SumPuntos(-PrecioCheckpoint);
            puntosRestados = true;
            Panelfalta.SetActive(false);
          }  
          else
          {
            Panelfalta.SetActive(true);
            StartCoroutine(ApagaDespuesdeTiempo());
          }
      }
    }
   }

    private IEnumerator ApagaDespuesdeTiempo()
    {
        Panelfalta.SetActive(true);
        // Wait for 2 seconds (adjust to your liking)
        yield return new WaitForSeconds(5f);

        // Destroy game object
        Panelfalta.SetActive(false);
    }
}

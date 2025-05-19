using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovEnemigo : MonoBehaviour
{
    [SerializeField] public float velocidad;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private float distancia;
    [SerializeField] private bool movimientoDerecha;
    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
         RaycastHit2D informacionSuelo = Physics2D.Raycast(controladorSuelo.position, Vector2.down, distancia);
        rb.velocity = new Vector2(velocidad, rb.velocity.y);
        //animator.SetBool("EstaCorriendo", true);

        if (informacionSuelo ==false)
        {
            Girar();
        }
    }


    private void Girar()
    {
        movimientoDerecha = !movimientoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocidad *= -1;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorSuelo.transform.position, controladorSuelo.transform.position + Vector3.down * distancia);
    }

     private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Suelo"))
        {
           Girar();  
        }
        if(other.gameObject.CompareTag("Enemigo"))
        {
           Girar();  
        }
    }
}

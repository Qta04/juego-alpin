using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoAlJugador : MonoBehaviour
{
    [SerializeField] private GameObject efecto; 
    public int DañoDelEnemigo;
    public GameObject drops;
    public float VidaEnemigo;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Daño"))
        {
            other.gameObject.GetComponent<ControladorPlayer>().TomarDaño(DañoDelEnemigo);
        }

        if(other.gameObject.CompareTag("Player"))
        {
            if (other.GetContact(0).normal.y <= -0.9)
            {
                animator.SetTrigger("Golpe");
                    other.gameObject.GetComponent<ControladorPlayer>().Rebote();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<ControladorPlayer>().TomarDaño(DañoDelEnemigo);
            
        }
    }
    

    public void Golpe()
    {
        Instantiate(efecto, transform.position, transform.rotation);
        Instantiate(drops, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void TomarDañoEnemigo(float daño)
    {
        MovEnemigo movEnemigo = GetComponent<MovEnemigo>();
        VidaEnemigo -= daño;
        if (VidaEnemigo <= 0)
        {
            animator.SetBool("EstaMuerto", true);
            movEnemigo.velocidad = 0;
            StartCoroutine(DestroyAfterDelay());
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        GameObject dropAleatorio = drops;
        Instantiate(dropAleatorio, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}


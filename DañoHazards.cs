using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoHazards : MonoBehaviour
{
   
    public int DañoDelEnemigo;
   
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ControladorPlayer>().TomarDaño(DañoDelEnemigo);
        }
    }
}

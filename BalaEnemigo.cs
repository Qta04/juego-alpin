using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BalaEnemigo : MonoBehaviour
{
    public float velocidad;
    public int daño;
    private void Update()
    {
        transform.Translate(Time.deltaTime * velocidad * Vector2.right);
    } 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ControladorPlayer>().TomarDaño(daño);
            Destroy(gameObject);
        }
         if(other.gameObject.CompareTag("Suelo"))
        {
            Destroy(gameObject);
        }
    }
}

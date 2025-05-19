using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControladorPlayer : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private float vida;
    
    [SerializeField] private float maximoVida;

    [Header("Rebote")]
    [SerializeField] private float velocidadRebote;
    
    [Header("Movimiento")] 

    private float inputMovimiento;
    public float velocidad;
    public float fuerzaSalto;

    public int saltosMaximos;
    public int saltosRestantes;

    [Header("SaltoPared")]

    [SerializeField] private Transform controladorPared;

    [SerializeField] private Vector3 dimensionesCajaPared;

    private bool enPared;
    private bool Saltando;
    private bool deslizando;
    [SerializeField] private float velocidadDeslizar;


    [Header("Capas")]
   public LayerMask capaSuelo;

   private Rigidbody2D rigidBody;
   private BoxCollider2D boxCollider;
   //private bool mirandoDerecha = true;
   private Animator animator;

   public SpriteRenderer spr;
   public GameObject efecto;

   [Header("PowerUp")]

   public float tiempoPowerUp;
   public float saltoExtra;
   public float velocidadExtra;
   public float vidaExtra;

   private EntradasMovimiento entradasMovimiento;


   //public string Linterna;

   //public Light linteral;

   Vector2 posicionInicial;
   [SerializeField] private GameObject PanelPowerUpSalto;
   [SerializeField] private GameObject PanelPowerUpVelocidad;
   [SerializeField] private GameObject PanelPowerUpVida;

   private void Awake()
   {
    entradasMovimiento = new EntradasMovimiento();
   }

   private void OnEnable()
   {
    entradasMovimiento.Enable();
   }

   private void OnDisable()
   {
    entradasMovimiento.Disable();
   }

   private void Start()
   {
       rigidBody = GetComponent<Rigidbody2D>();
       boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        
        saltosRestantes = saltosMaximos;
        vida = maximoVida;
        posicionInicial = transform.position;

        
   }
    void Update()
    {
        Movimiento();
        Salto();
        Deslizando();
        //Prender();

        //entradasMovimiento.Movimiento.Salto.performed += contexto =>Salto(contexto);

        //float inputMovimiento = entradasMovimiento.Movimiento.Horizontal.ReadValue<float>();
        animator.SetBool("Deslizando", deslizando);
        
    }

    bool EstaEnSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return raycastHit.collider != null;
    }

    public void BotonSalto()
    {
        RealizarSalto();
    }

    void Salto()
    {
        

        if (EstaEnSuelo() )
        {
            saltosRestantes = saltosMaximos;
        }
       if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && saltosRestantes > 0)
       {
            RealizarSalto();
            
       }
       else
       {
        animator.SetBool("EstaSaltando", false);
       }

       if (!EstaEnSuelo())
       {
            animator.SetBool("EstaSaltando", true);       
       }
       else
       {
            animator.SetBool("EstaSaltando", false);
       }
    }

    public void RealizarSalto()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
        rigidBody.AddForce(Vector2.up * CalcularSaltoTotal(), ForceMode2D.Impulse);
        saltosRestantes--;
        animator.SetBool("EstaSaltando", true);
    }

    private float CalcularSaltoTotal()
    {
        return fuerzaSalto + saltoExtra;
    }

    public void SubirSaltoPowerUp(float saltoExtraParametro)
    {
        StartCoroutine(SubirSaltoPowerUpCoroutine(saltoExtraParametro));
    }

    private void FixedUpdate()
    {
        enPared = Physics2D.OverlapBox(controladorPared.position,dimensionesCajaPared,0f,capaSuelo); 
        

        if (deslizando)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Clamp(rigidBody.velocity.y, -velocidadDeslizar, float.MaxValue));
        }
    }

    void Deslizando()
    {
        if (!EstaEnSuelo() && enPared && inputMovimiento != 0)
        {
            deslizando = true;
             if(inputMovimiento < 0.1f){
                spr.flipX = false;
                // hijoTransform.rotation = Quaternion.Euler(0 , 90, 1); 
                }
             if(inputMovimiento > 0.1f){
                spr.flipX = true;
             //hijoTransform.rotation = Quaternion.Euler(0 , -90, 1);
                }
        }
        else
        {
            deslizando = false;
        }

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan; 
        Gizmos.DrawWireCube(controladorPared.position,dimensionesCajaPared);
        
        
    }

    public void Rebote()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, velocidadRebote); 
    }

    void Movimiento()
    {
        //Transform hijoTransform = transform.Find("Linterna");
        inputMovimiento = entradasMovimiento.Movimiento.Horizontal.ReadValue<float>();

        if (inputMovimiento != 0f)
        {
            animator.SetBool("EstaCorriendo", true);
        }
        else
        {
            animator.SetBool("EstaCorriendo", false);
        }
   
       
        rigidBody.velocity = new Vector2(inputMovimiento * CalcularVelocidadTotal(), rigidBody.velocity.y);
       
        
        //GestionarMovimiento(inputMovimiento);

        if(inputMovimiento > 0.1f){
            spr.flipX = false;
           // hijoTransform.rotation = Quaternion.Euler(0 , 90, 1); 
        }
        if(inputMovimiento < 0.1f){
            spr.flipX = true;
            //hijoTransform.rotation = Quaternion.Euler(0 , -90, 1);
        }
    }

   /* void GestionarMovimiento(float inputMovimiento)
    {
        if ((mirandoDerecha == true && inputMovimiento < 0)||(mirandoDerecha == false && inputMovimiento > 0))
        {
            mirandoDerecha = !mirandoDerecha;
            //transform.localScale = new Vector2(-transform.localScale.x,transform.localScale.y);

        }
    }*/
    private float CalcularVelocidadTotal()
    {
        return velocidad + velocidadExtra;
    }

    public void SubirVelocidadPowerUp(float velocidadExtraParametro)
    {
        StartCoroutine(SubirVelocidadPowerUpCoroutine(velocidadExtraParametro));
    }


    public void TomarDaño(float daño)
    {
        vida -= daño;
        
        if (CalcularVidaTotal() <= 0)
        {
            StartCoroutine(DestroyAfterDelay());
            
        }
    }
    public void Curar(float curacion)
    {
        if ((vida + curacion) > maximoVida)
        {
            vida = maximoVida;
        }
        else
        {
            vida += curacion;
        }
        
    }

     private float CalcularVidaTotal()
    {
        return vida + vidaExtra;
    }

    public void SubirVidaPowerUp(float vidaExtraParametro)
    {
        StartCoroutine(SubirVidaPowerUpCoroutine(vidaExtraParametro));
    }

    public void CargarCheckpoint(Vector2 posicion)
    {
        posicionInicial = posicion; 
    }

    public void Respawn()
    {
        transform.position = posicionInicial;
        
    }

    private IEnumerator DestroyAfterDelay()
    {
        animator.SetTrigger("Muerte");
        Instantiate(efecto, transform.position, Quaternion.identity);
        // Wait for 2 seconds (adjust to your liking)
        yield return new WaitForSeconds(0.42f);

        // Destroy game object
        Respawn();
    }

    private IEnumerator SubirSaltoPowerUpCoroutine(float saltoExtraParametro)
    {
        PanelPowerUpSalto.SetActive(true);
        saltoExtra = saltoExtraParametro;
        Debug.Log ("PowerUp inicio");    //aca se activa los iconos de los efectos, en el canvas
        yield return new WaitForSeconds(tiempoPowerUp);
        saltoExtra = 0;
        Debug.Log ("PowerUp Fin");
        PanelPowerUpSalto.SetActive(false);
    }

    private IEnumerator SubirVelocidadPowerUpCoroutine(float velocidadExtraParametro)
    {
        PanelPowerUpVelocidad.SetActive(true);
        velocidadExtra = velocidadExtraParametro;
        Debug.Log ("PowerUp inicio");    //aca se activa los iconos de los efectos, en el canvas
        yield return new WaitForSeconds(tiempoPowerUp);
        velocidadExtra = 0;
        Debug.Log ("PowerUp Fin");
        PanelPowerUpVelocidad.SetActive(false);
    }

    private IEnumerator SubirVidaPowerUpCoroutine(float vidaExtraParametro)
    {
        PanelPowerUpVida.SetActive(true);
        vidaExtra = vidaExtraParametro;
        Debug.Log ("PowerUp inicio");    //aca se activa los iconos de los efectos, en el canvas
        yield return new WaitForSeconds(tiempoPowerUp);
        vidaExtra = 0;
        Debug.Log ("PowerUp Fin");
        PanelPowerUpVida.SetActive(false);
    }
    /*public void linternaON()
    {
        linteral.enabled = !linteral.enabled;
    }*/
}

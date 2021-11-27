using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
 

public class CharacterController1 : MonoBehaviour
{
    //Sirve para el movimiento horizontal
    public float speed = 1;

    //Sirve para la intensidad del salto
    public float jumpForce = 1;

    //Sirve para regular el rebote del rebotín
    public float rebote = 2;

    //Sirve para limitar el salto y doble salto
    public int cantidadDeSaltos = 0;

    //Sirve para el clamp limitador de fuerza de movimiento horizontal
    public float maxVel = 4;

    //Sirven para el Raycast
    public Transform groundCheck; //para el hijo vacío del player que tiene el Raycast en su bordecito
    public float groundDistance = 1;

    //Para que el raycast detecte solo lo de la layer "piso"
    public LayerMask piso;

    //Para que el raycast detecte solo lo de la layer "rebotin"
    public LayerMask rebotin;

    //Para poder llamar al rigidbody del player uwu
    public Rigidbody2D rb2d;

    //Para que se vea la puerta abierta cuando ganas
    public GameObject puertaAbierta;

        
    
    void Start()
    {
        //Para poder usar el rigidbody del player en la progra uwu
        rb2d = GetComponent<Rigidbody2D>();

        
    }

           
    void Update()
    {
        //Movimiento horizontal 
        var movement = Input.GetAxis("Horizontal");
        rb2d.AddForce(new Vector2(movement, 0), ForceMode2D.Impulse);

        //Salto y doble salto en plataformas de layer "piso"    
        if (Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, 1 << LayerMask.NameToLayer("piso")))
        {
            cantidadDeSaltos = 1;
                        
        }
               
        if (Input.GetButtonDown("Jump") && (cantidadDeSaltos > 0))
        {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            cantidadDeSaltos--;

        }

        //Rebote en rebotines de layer "rebotin"    
        if (Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, 1 << LayerMask.NameToLayer("rebotin")))
        {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(new Vector2(0, jumpForce*rebote), ForceMode2D.Impulse);

        }
                

        //limitador de fuerza de movimiento
        Vector2 vel = rb2d.velocity;
        vel.x = Mathf.Clamp(vel.x, -maxVel, maxVel);
        rb2d.velocity = vel;
    }

    //que se vea el Raycast del player
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 startPosition = groundCheck.position;
        Vector2 endPosition = startPosition + new Vector2(0, -1 * groundDistance);
        Gizmos.DrawLine(startPosition, endPosition);
    }


    //muerte de Player por Pinxos o Enemigo2
    private void OnTriggerEnter2D(Collider2D enemigo)
    {
        
        if (enemigo.CompareTag("Pinxo") || enemigo.CompareTag("Enemigo2") || enemigo.CompareTag("Bala"))
        {
            Debug.Log("kiwi");
            Invoke("ReiniciarEscena", 0.5f);
            gameObject.SetActive(false);
            

        }

        //Player WINNNNNNNNNNNNNNNNNNNNNN
        if (enemigo.CompareTag("Win"))
        {
            Debug.Log("win");
            puertaAbierta.SetActive(true);
            Invoke("ReiniciarEscena", 1f);          


        }

    }


    

    //Reseteo de nivel >:3
    void ReiniciarEscena()
    {
        SceneManager.LoadScene("Level101");        
    }

   

}

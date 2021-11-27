using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemigo2 : MonoBehaviour
{
    public float velocidad = 0;   
    public float distanciaDelJugador;
    public float rangoDeVision;   

    public Transform player;
    public Rigidbody2D rbe2;

    public float movimientoEmpujador = 1.5f;
    public Tween patrulla;
    

    private void Start()
    {
        //Movimiento del enemigo
        patrulla = transform.DOMoveX(movimientoEmpujador, 1).SetRelative(true).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

    }

    private void Update()
    {
        //Que persiga al player cuando está en su rango de visión
        distanciaDelJugador = Vector2.Distance(player.position, rbe2.position);
        if(distanciaDelJugador < rangoDeVision)
        {
            Vector2 objetivo = new Vector2(player.position.x, player.position.y);
            Vector2 nuevaPos = Vector2.MoveTowards(rbe2.position, objetivo, velocidad * Time.deltaTime);
            rbe2.MovePosition(nuevaPos);
            patrulla.Pause();
           
        }
        
    }

    private void OnDrawGizmos()
    {
        //Que se dibuje el rango de visión del enemigo
        Gizmos.color = Color.blue;       
        Gizmos.DrawWireSphere(transform.position, rangoDeVision);       
    }
}


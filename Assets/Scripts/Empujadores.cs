using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Empujadores : MonoBehaviour
{
    public float movimientoEmpujador = 1.5f;
    public float fuerzaEmpuj�n = 10;
    public Rigidbody2D rbPlayer;
    public GameObject holaPlayer;
     
        
    void Start()
    {
        //Movimiento del empujador
        transform.DOMoveX(movimientoEmpujador, 1).SetRelative(true).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutBack);

        //Robarme el rigidbody del player uwu
        rbPlayer = holaPlayer.GetComponent<Rigidbody2D>();

    }


    //Me empuj�oooooooooo OvO
    private void OnCollisionEnter2D(Collision2D player)
    {
        if (player.collider)
        {               
            rbPlayer.velocity = Vector2.zero;
            rbPlayer.AddForce(new Vector2(fuerzaEmpuj�n, 3), ForceMode2D.Impulse);
        }
    }
       

}

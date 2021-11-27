using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaEstatica : MonoBehaviour
{
    public Transform target;
    public Transform torretaEstatica;

    public GameObject spawner;
    public GameObject bala;
    GameObject balaClon;
    public float fuerzaBala;
    public bool estaEnLaZona;

    public float distanciaDelJugador;
    public float rangoDeVision;

    

    

    // Update is called once per frame
    void Update()
    {
        //Que dispare al player cuando está en su rango de visión
        distanciaDelJugador = Vector2.Distance(target.position, torretaEstatica.position);
        if (distanciaDelJugador < rangoDeVision)
        {            

            if (estaEnLaZona == false)
            {
                estaEnLaZona = true;
                StartCoroutine("CadaXTiempoDisparo");
            }                      

        }
        else
        {
            estaEnLaZona = false;
        }


        //que lo siga con la rotación rara
        Vector3 dif = target.position - transform.position;
        dif.Normalize();
        float rote_z = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, rote_z - 90);
                

    }

    //la función de la bala con su addforce
    private void Disparo()
    {
        balaClon = Instantiate(bala, spawner.GetComponent<Transform>().position, Quaternion.identity);        
        balaClon.GetComponent<Rigidbody2D>().AddForce(transform.up * fuerzaBala, ForceMode2D.Impulse); 

    }

    private void OnDrawGizmos()
    {
        //Que se dibuje el rango de visión de disparo del enemigo
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangoDeVision);
    }

    //la corrutina para que la bala salga cada 1 segundo
    IEnumerator CadaXTiempoDisparo()
    {
        while (estaEnLaZona == true)
        {
            Disparo();
            yield return new WaitForSeconds(1);
        }
        
        yield break; 

    }
}

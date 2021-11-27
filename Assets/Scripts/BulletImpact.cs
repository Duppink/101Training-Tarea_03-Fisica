using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    
    //destruir la bala al impactar
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pinxo") || other.CompareTag("Floor") || other.CompareTag("Otro"))
        {
            Debug.Log("impacta3");
            Destroy(gameObject);
        }
            
        
    }

   
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarCOntroller : MonoBehaviour
{
    public string tagDetectar = "Player";
    public List<Collider2D> detectarObj = new List<Collider2D>();
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == tagDetectar)
        {
            detectarObj.Add(col);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == tagDetectar)
        {
            detectarObj.Remove(col);
        }
    }
}

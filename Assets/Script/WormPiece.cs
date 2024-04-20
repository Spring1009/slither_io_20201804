using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormPiece : MonoBehaviour
{
    public Worm Worm;

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Player"){
            Worm.AddFollower();
            Destroy(gameObject, 0.02f);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour {
   
    public float effectLenght;

    void Start () {

    }

    
    void Update () {
        Destroy(gameObject, effectLenght);
    }

}
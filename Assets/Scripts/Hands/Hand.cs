using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    Animator animator;

    //Aqui puedo meter el hitbox de la mano despues

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void SetClosed(bool state)
    {
        animator.SetBool("Close", state);
    }

}

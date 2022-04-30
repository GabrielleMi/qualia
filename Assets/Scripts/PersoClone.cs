using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersoClone : Perso
{

    protected override void Destroyed()
    {
        Destroy(gameObject);
    }

}

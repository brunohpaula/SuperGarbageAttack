using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarPoo : MonoBehaviour {

    public bool poo = false;

    public bool GetPoo()
    {
        return poo;
    }

    public void SetPoo(bool b)
    {
        poo = b;
    }

}

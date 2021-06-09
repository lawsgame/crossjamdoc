using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Factory;

public class Mines : MonoBehaviour
{
    public int Quantity;

    [SerializeField] private Ressource type;
    
    public Ressource Type => type;
}

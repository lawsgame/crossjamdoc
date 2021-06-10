using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Factory;

public class PickableResource : MonoBehaviour
{
    [SerializeField] private Ressource type;
    [SerializeField] private int quantity;

    public Ressource Type => type;
    public int Quantity => quantity;

}

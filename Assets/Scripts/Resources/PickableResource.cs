using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Factory;

public class PickableResource : MonoBehaviour
{
    [SerializeField] private Ressource type;

    public Ressource Type => type;

}

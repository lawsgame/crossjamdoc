using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Factory;

public class Mines : MonoBehaviour
{
    [SerializeField] private GameObject QuantityIndicator;
    [SerializeField] private Ressource type;
    [SerializeField] private int quantity;

    public int Quantity => quantity;
    
    private TextMeshPro textHolder;

    public Ressource Type => type;

    private void Start()
    {
        textHolder = QuantityIndicator.GetComponent<TextMeshPro>();
        textHolder.SetText(string.Format("{0}", quantity));
    }

    public void ChangeQuantity(int amountAdded)
    {
        quantity += amountAdded;
        textHolder.SetText(string.Format("{0}", quantity));
    }
}

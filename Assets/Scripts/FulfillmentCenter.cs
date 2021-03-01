using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FulfillmentCenter : MonoBehaviour {
    [SerializeField]
    private List<Product> supply;

    [SerializeField]
    private List<Vehicle> vehicles;

    private bool counted;

    public delegate void AddFCAction(FulfillmentCenter fc);
    public static event AddFCAction OnAddFC;

    public delegate void RemoveFCAction(FulfillmentCenter fc);
    public static event RemoveFCAction OnRemoveFC;

    // Awake is called before the script is enabled, and before Start
    private void Awake() {
        counted = false;
    }

    // Start is called before the first Update
    private void Start() {
        foreach (Vehicle vehicle in vehicles) {
            vehicle.Source = this;
        }
    }

    // Update is called once per frame
    private void Update() {
        
    }

    private void OnMouseDown() {
        counted = !counted;
        if (counted) {
            OnAddFC(this);
        } else {
            OnRemoveFC(this);
        }
    }

    public bool ContainsSupply(Product product) {
        return supply.Contains(product);
    }

    public void ShipVehicle(DemandDestination destination) {
        if (!ContainsSupply(destination.RequestedProduct)) {
            throw new Exception("Cannot ship product that we do not have: " + destination.RequestedProduct.ID);
        }

        foreach (Vehicle vehicle in vehicles) {
            if (!vehicle.Away) {
                vehicle.Destination = destination;
                vehicle.AddProduct(supply.Pop(destination.RequestedProduct));
                return;
            }
        }

        throw new Exception("Cannot ship product without any vehicles");
    }
}

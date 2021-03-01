using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FulfillmentCenter : MonoBehaviour {
    [SerializeField]
    private List<Product> supply;

    [SerializeField]
    private List<Vehicle> vehicles;

    // Awake is called before the script is enabled, and before Start
    private void Awake() {
        
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

    private void OnEnable() {
        FulfillmentSolver.Instance.AddFC(this);
    }

    private void OnDisable() {
        FulfillmentSolver.Instance.RemoveFC(this);
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

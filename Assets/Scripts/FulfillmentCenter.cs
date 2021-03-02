using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FulfillmentCenter : PanelCreator {
    public delegate void AddFCAction(FulfillmentCenter fc);
    public static event AddFCAction OnAddFC;

    public delegate void RemoveFCAction(FulfillmentCenter fc);
    public static event RemoveFCAction OnRemoveFC;

    [SerializeField]
    private List<Product> initialSupply;

    [SerializeField]
    private List<Vehicle> vehicles;

    private StorageDict<Product> supply;
    private bool counted;
    
    private Product ProductWithMostSupply { get => supply.ItemWithHighestCount;  }

    // Awake is called before the script is enabled, and before Start
    private void Awake() {
        counted = false;
        supply = new StorageDict<Product>(initialSupply);
    }

    // Start is called before the first Update
    private void Start() {
        foreach (Vehicle vehicle in vehicles) {
            vehicle.Source = this;
        }
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            counted = !counted;
            if (counted) {
                OnAddFC(this);
            } else {
                OnRemoveFC(this);
            }
        }
    }

    public override UIPanel CreatePanel() {
        UIPanel result = new UIPanel("FC");
        result.AddAttribute("Product with Most Supply", ProductWithMostSupply == null ? "None" : ProductWithMostSupply.Name);
        result.AddAttribute("Enabled for Delivery", counted.ToString());

        return result;
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
                vehicle.AddProduct(supply.PopItem(destination.RequestedProduct));
                return;
            }
        }

        throw new Exception("Cannot ship product without any vehicles");
    }
}

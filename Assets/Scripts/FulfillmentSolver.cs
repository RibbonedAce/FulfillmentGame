using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FulfillmentSolver : MonoBehaviour {
    public static FulfillmentSolver Instance { get; private set; }

    private List<FulfillmentCenter> fcs;

    // Awake is called before the script is enabled, and before Start
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogWarning("Found extra FulfillmentSolver on" + name);
            enabled = false;
        }

        fcs = new List<FulfillmentCenter>();
        FulfillmentCenter.OnAddFC += (fc) => fcs.Add(fc);
        FulfillmentCenter.OnRemoveFC += (fc) => fcs.Remove(fc);
    }

    public void PlaceOrder(DemandDestination destination) {
        foreach (FulfillmentCenter fc in fcs) {
            if (fc.ContainsSupply(destination.RequestedProduct)) {
                fc.ShipVehicle(destination);
                return;
            }
        }

        Debug.LogWarning("Could not fulfill order for Product ID " + destination.RequestedProduct.ID + " to " + destination.name);
    }
}

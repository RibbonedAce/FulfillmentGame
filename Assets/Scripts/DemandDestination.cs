using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemandDestination : PanelCreator {
    [SerializeField]
    private float orderInterval;

    [SerializeField]
    private Product orderProduct;

    private Product requestedProduct;
    public Product RequestedProduct { get => requestedProduct; }

    private float timeSinceOrder;

    // Awake is called before the script is enabled, and before Start
    private void Awake() {
        timeSinceOrder = 0;
    }

    // Start is called before the first Update
    private void Start() {
        
    }

    // Update is called once per frame
    private void Update() {
        if (requestedProduct == null) {
            timeSinceOrder += Time.deltaTime;
        }

        if (timeSinceOrder > orderInterval) {
            PlaceOrder();
        }
    }

    public override UIPanel CreatePanel() {
        UIPanel result = new UIPanel(name);
        result.AddAttribute("Ordered", requestedProduct == null ? "None" : requestedProduct.Name);

        return result;
    }

    public void Pickup(Product product) {
        if (product != null) {
            requestedProduct = null;
        }
    }

    private void PlaceOrder() {
        timeSinceOrder = 0;
        requestedProduct = orderProduct;
        FulfillmentSolver.Instance.PlaceOrder(this);
    }
}

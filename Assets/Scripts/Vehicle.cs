using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : PanelCreator {
    [SerializeField]
    private float speed;

    private StorageDict<Product> load;
    private Transform target;

    private FulfillmentCenter source;
    public FulfillmentCenter Source {
        get => source;
        set {
            source = value;
        }
    }

    private DemandDestination destination;
    public DemandDestination Destination {
        get => destination;
        set {
            destination = value;
        }
    }

    public bool Away { get => load.Count > 0 || Vector3.Distance(transform.position, source.transform.position) > 0.001; }

    // Awake is called before the script is enabled, and before Start
    private void Awake() {
        load = new StorageDict<Product>();
    }

    // Start is called before the first Update
    private void Start() {

    }

    // Update is called once per frame
    private void Update() {
        if (destination == null) {
            GoTowardsTarget(source.transform);
            return;
        }

        GoTowardsTarget(destination.transform);
        if (Vector3.Distance(transform.position, destination.transform.position) < 0.001) {
            Dropoff(destination.RequestedProduct);
        }
    }
    public override UIPanel CreatePanel() {
        UIPanel result = new UIPanel("Truck");
        result.AddAttribute("Heading to", target.name);
        result.AddAttribute("Carrying", load.Count.ToString());

        return result;
    }

    public void AddProduct(Product product) {
        load.AddItem(product);
    }

    public void Dropoff(Product product) {
        destination.Pickup(load.PopItem(product));
        destination = null;
    }

    private void GoTowardsTarget(Transform target) {
        this.target = target;
        transform.LookAt2D(target, true);

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > 0.001) {
            if (distance < speed * Time.deltaTime) {
                transform.position = target.position;
            } else {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
    }
}

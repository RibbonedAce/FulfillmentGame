using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Product {
    [SerializeField]
    private long id;
    public long ID { get => id; }

    [SerializeField]
    private string name;
    public string Name { get => name; }

    [SerializeField]
    private double baseValue;
    public double BaseValue { get => baseValue; }

    public Product(long id, string name, double baseValue) {
        this.id = id;
        this.name = name;
        this.baseValue = baseValue;
    }

    public override bool Equals(object obj) {
        return obj is Product product &&
               id == product.id;
    }

    public override int GetHashCode() {
        return 1877310944 + id.GetHashCode();
    }
}

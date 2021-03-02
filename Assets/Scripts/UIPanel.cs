using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel {
    private readonly string title;

    public string Title { get => title; }

    private readonly List<KeyValuePair<string, string>> attributes;
    public List<KeyValuePair<string, string>> Attributes { get => new List<KeyValuePair<string, string>>(attributes); }

    public UIPanel(string title) {
        this.title = title;
        this.attributes = new List<KeyValuePair<string, string>>();
    }

    public void AddAttribute(string key, string value) {
        attributes.Add(new KeyValuePair<string, string>(key, value));
    }
}

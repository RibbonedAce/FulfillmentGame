using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static UIController Instance { get; private set; }

    [SerializeField]
    private Transform uiObject;

    private Text title;
    private Text[] keys;
    private Text[] values;

    // Awake is called before the script is enabled, and before Start
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogWarning("Found extra UIController on" + name);
            enabled = false;
        }

        PanelCreator.OnClickObject += DisplayPanel;

        title = uiObject.Find("Title").GetComponent<Text>();
        keys = uiObject.Find("Keys").GetComponentsInChildren<Text>();
        values = uiObject.Find("Values").GetComponentsInChildren<Text>();
    }

    // Start is called before the first Update
    private void Start() {
        
    }

    // Update is called once per frame
    private void Update() {
        
    }
    private void DisplayPanel(UIPanel uiPanel) {
        title.text = uiPanel.Title;

        for (int i = 0; i < keys.Length; ++i) {
            if (i < uiPanel.Attributes.Count) {
                keys[i].text = uiPanel.Attributes[i].Key;
                values[i].text = uiPanel.Attributes[i].Value;
            } else {
                keys[i].text = "";
                values[i].text = "";
            }
        }
    }
}
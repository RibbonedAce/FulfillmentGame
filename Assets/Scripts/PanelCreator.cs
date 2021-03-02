using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class PanelCreator : MonoBehaviour {
    public delegate void ClickObjectAction(UIPanel uiPanel);
    public static event ClickObjectAction OnClickObject;

    protected virtual void OnMouseDown() {
        OnClickObject(CreatePanel());
    }

    public abstract UIPanel CreatePanel();
}

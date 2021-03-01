using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraTrack : MonoBehaviour {
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset;

    private Camera _camera;

    // Awake is called before the script is enabled, and before Start
    private void Awake() {
        _camera = GetComponent<Camera>();
    }

    // Start is called before the first Update
    private void Start() {
        
    }

    // Update is called once per frame
    private void Update() {
        transform.position = target.position + offset;
    }
}

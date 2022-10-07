using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Laser;
using UnityEditor.U2D.Path;
using UnityEngine;

public class MousePoint : MonoBehaviour
{
    public static MousePoint Mouse;

    public Vector3 MousePosition { private set; get; }
    [SerializeField] private LaserHead drag;
    private LaserHead _drag;

    private void Awake() {
        if (!Mouse) {
            Mouse = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        //TODO: object or not? OrderProblem?
        _drag = Instantiate(drag, Vector3.zero, Quaternion.Euler(Vector3.zero));
    }

    void Update() {
        if(!_drag.GetActivity()) _drag.GenerateLaser(10,0,0.15f,64f,this.transform);
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePosition = new Vector3(pos.x, pos.y, 0f);
        transform.position = MousePosition;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}

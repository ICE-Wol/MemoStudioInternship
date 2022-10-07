using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Laser;
using UnityEngine;

public class MousePoint : MonoBehaviour
{
    public static MousePoint Mouse;

    public Vector3 MousePosition { private set; get; }
    [SerializeField] private LaserHead drag;
    private LaserHead[] _dragList;
    
    private void Awake() {
        if (!Mouse) {
            Mouse = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        //TODO: object or not?
        _dragList = new LaserHead[1];
        _dragList[0] = Instantiate(drag, this.transform);
        
        _dragList[0].GenerateLaser(10,0,0.1f,64f,this.transform);
    }

    void Update() {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePosition = new Vector3(pos.x, pos.y, 0f);
        transform.position = MousePosition;
    }
}

using System;
using _Scripts.Function;
using TMPro;
using UnityEngine;

namespace _Scripts.Laser {
    public class LaserHead : MonoBehaviour {
        [SerializeField] private LaserUnit laserUnit;
        private MeshFilter _filter;
        private Transform _follow;
        private float _followRate;
        
        private Mesh _mesh;
        private int _length;
        private float _color;
        private float _radius;
        private bool _isActivated;

        private LaserUnit[] _units;
        private Vector3[] _vertices;
        private Vector2[] _uv;
        private int[] _triangles;

        private void Start() {
            transform.position = Vector3.zero;
            _filter = GetComponent<MeshFilter>();
            _mesh = new Mesh();
        }

        public void GenerateLaser(int len,int col,float rad,float rate,Transform tf) {
            _length = len;
            _color = col;
            _radius = rad;
            _follow = tf;
            _followRate = rate;
            
            _units = new LaserUnit[_length];
            _vertices = new Vector3[_length * 2];
            _uv = new Vector2[_length * 2];
            _triangles = new int[(_length - 1) * 2 * 3];

            for (int i = 0; i < _length; i++) {
                _units[i] = LaserManager.Manager.LaserPool.Get();
                _units[i].head = this;
                _units[i].order = i;
                _units[i].isActive = !(i == 0 || i == _length - 1);
            }

            _isActivated = true;
        }
        
        

        public void ReleaseLaser() {
            _isActivated = false;
            for (int i = 0; i < _length; i++) {
                LaserManager.Manager.LaserPool.Release(_units[i]);
            }
        }

        public Vector3 GetPreUnitPosition(int cur) => (cur == 0) ? Vector3.zero : _units[cur - 1].transform.position;
        public Transform GetFollowTransform() => _follow;
        public float GetFollowRate() => _followRate;

        private void RefreshMesh() {
            for (int i = 0; i < _length; i++) {
                float col = 1 - (_color + 1) / 16f;
                _uv[i * 2] = new Vector2(1f / (_length - 1) * i, col + 1 / 16f);
                _uv[i * 2 + 1] = new Vector2(1f / (_length - 1) * i, col);
                
                Vector3 curPos = _units[i].transform.position;
                if (!_units[i].isActive) {
                    _vertices[i * 2] = curPos;
                    _vertices[i * 2 + 1] = curPos;
                }
                else {
                    Vector3 nxtPos = _units[i + 1].transform.position;
                    Vector3 offset = (nxtPos - curPos).normalized * _radius;
                    Vector3 normal2D = new Vector3(0, 0, -1f);
                    //float angle = Vector2.Angle(Vector2.right, (nxtPos - curPos).normalized) + 90f;
                    //Vector2 offset = _radius * (Calc.Degree2Direction(angle).normalized);
                    //Vector2 offset = Vector2.up;
                    _vertices[i * 2] = curPos + Vector3.Cross(offset,normal2D);
                    _vertices[i * 2 + 1] = curPos + Vector3.Cross(offset,normal2D * -1f);
                }

                if(i == _length - 1) continue;
                
                //1. clock-wise or it will not render.
                //2.important: 6 points in a group.
                _triangles[i * 6] = i * 2;
                _triangles[i * 6 + 1] = (i + 1) * 2 + 1;
                _triangles[i * 6 + 2] = i * 2 + 1;

                _triangles[i * 6 + 3] = i * 2;
                _triangles[i * 6 + 4] = (i + 1) * 2;
                _triangles[i * 6 + 5] = (i + 1) * 2 + 1;
            }
            
            _mesh.vertices = _vertices;
            _mesh.uv = _uv;
            _mesh.triangles = _triangles;
            _filter.mesh = _mesh;
        }

        private void Update() {
            if (_isActivated) {
                if (_follow == null) {
                    ReleaseLaser();
                }
                RefreshMesh();
            }
        }
    }
}
using System;
using _Scripts.Function;
using TMPro;
using UnityEngine;

namespace _Scripts.Laser {
    public class LaserHead : MonoBehaviour {
        [SerializeField] private LaserUnit laserUnit;
        private MeshFilter _filter;
        
        private Mesh _mesh;
        private int _length;
        private float _radius;
        private bool _isActivated;

        private LaserUnit[] _units;
        private Vector3[] _vertices;
        private Vector2[] _uv;
        private int[] _triangles;

        private void Start() {
            _filter = GetComponent<MeshFilter>();
            _mesh = new Mesh();
            _isActivated = true;

            /*_length = 2;
            _vertices = new Vector3[_length * 2];
            _uv = new Vector2[_length * 2];
            _triangles = new int[(_length - 1) * 2 * 3];
            
            _vertices[0] = Vector3.zero;
            _vertices[1] = Vector3.up * 100;
            _vertices[2] = Vector3.right * 100 + Vector3.up * 100;
            _vertices[3] = Vector3.right * 100;

            _uv[0] = Vector2.zero;
            _uv[1] = Vector2.up;
            _uv[2] = Vector2.one;
            _uv[3] = Vector2.right;

            _triangles[0] = 0;
            _triangles[1] = 1;
            _triangles[2] = 2;
            
            _triangles[3] = 0;
            _triangles[4] = 2;
            _triangles[5] = 3;
            
            _mesh.vertices = _vertices;
            _mesh.uv = _uv;
            _mesh.triangles = _triangles;
            _filter.mesh = _mesh;*/
        }

        public void GenerateLaser(int len) {
            _length = len;
            
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

        public LaserUnit GetPreUnit(int cur) => (cur == 0) ? null : _units[cur - 1];

        private void RefreshMesh() {
            for (int i = 0; i < _length; i++) {
                _uv[i * 2] = new Vector2(1f / (_length - 1) * i, 0);
                _uv[i * 2 + 1] = new Vector2(1f / (_length - 1) * i, 1 / 16f);
                
                Vector2 curPos = _units[i].transform.position;
                if (!_units[i].isActive) {
                    _vertices[i * 2] = curPos;
                    _vertices[i * 2 + 1] = curPos;
                }
                else {
                    Vector2 nxtPos = _units[i + 1].transform.position;
                    float angle = Vector2.Angle(Vector2.right, nxtPos - curPos) + 90f;
                    Vector2 offset = _radius * Calc.Degree2Direction(angle);
                    //Vector2 offset = Vector2.up;
                    _vertices[i * 2] = curPos + offset;
                    _vertices[i * 2 + 1] = curPos - offset;
                }

                if(i == _length - 1) continue;
                
                _triangles[i * 3] = i * 2 + 1;
                _triangles[i * 3 + 1] = i * 2;
                _triangles[i * 3 + 2] = (i + 1) * 2;
                
                _triangles[(i + 1) * 3] = i * 2 + 1;
                _triangles[(i + 1) * 3 + 1] = (i + 1) * 2 + 1;
                _triangles[(i + 1) * 3 + 2] = (i + 1) * 2;
            }
            
            _mesh.vertices = _vertices;
            _mesh.uv = _uv;
            _mesh.triangles = _triangles;
            _filter.mesh = _mesh;
        }

        private void Update() {
            
            _radius = 0.5f;
            if (_isActivated) {
                RefreshMesh();
            }
        }
        
        private void OnDrawGizmos() {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.red;
            //Gizmos.DrawSphere(transform.position, 1f);
        }
    }
}
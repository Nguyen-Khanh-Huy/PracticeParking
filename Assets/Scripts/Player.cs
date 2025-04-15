using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private CapsuleCollider2D _playerCollider;
    [SerializeField] private BoxCollider2D _endPointCollider;
    [SerializeField] private int _curPointIdx = 0;
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private bool _isMoving;
    [SerializeField] private List<Vector3> _pathPoints;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Star"))
            other.gameObject.SetActive(false);

        if (other.gameObject.layer == LayerMask.NameToLayer("BG"))
            transform.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            DrawLine();
            _curPointIdx = 0;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_pathPoints.Count < 2 || !_endPointCollider.OverlapPoint(_pathPoints[^1]))
            {
                _pathPoints.Clear();
                _lineRenderer.positionCount = 0;
            }
            else _isMoving = true;
        }

        if (!_isMoving || _curPointIdx >= _pathPoints.Count) return;
        if (_isMoving)
            PlayerMoving();
    }

    private void PlayerMoving()
    {
        Vector3 target = _pathPoints[_curPointIdx];
        target.z = 0;
        transform.position = Vector3.MoveTowards(transform.position, target, _moveSpeed * Time.deltaTime);
        Vector3 direction = target - transform.position;
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            _pathPoints.RemoveAt(_curPointIdx);
            _lineRenderer.positionCount = _pathPoints.Count;
            _lineRenderer.SetPositions(_pathPoints.ToArray());
            if (_pathPoints.Count == 0)
            {
                _isMoving = false;
                Debug.Log("zzzzzz");
            }
        }
    }


    private void DrawLine()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePoint2D = new(mouseWorldPos.x, mouseWorldPos.y, 1f);
        if (_pathPoints.Count == 0)
        {
            if (_playerCollider.OverlapPoint(mousePoint2D))
            {
                _pathPoints.Add(mouseWorldPos);
                _lineRenderer.positionCount = 1;
                _lineRenderer.SetPosition(0, mouseWorldPos);
            }
        }
        else
        {
            if (Vector2.Distance(mousePoint2D, _pathPoints[_pathPoints.Count - 1]) > 0.1f)
            {
                _pathPoints.Add(mouseWorldPos);
                _lineRenderer.positionCount = _pathPoints.Count;
                _lineRenderer.SetPositions(_pathPoints.ToArray());
            }
        }
    }
}

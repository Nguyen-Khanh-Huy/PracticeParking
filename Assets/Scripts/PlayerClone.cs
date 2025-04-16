using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClone : Player
{
    [SerializeField] Player player;
    protected override void OnEnable()
    {
        base.OnEnable();
        player.PlayerClone = this;
    }
    protected override void Update()
    {
        if (Input.GetMouseButton(0) && !IsMoving && !IsFinish)
        {
            DrawLine();
            _curPointIdx = 0;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_pathPoints.Count < 2 || !EndPointCollider.OverlapPoint(_pathPoints[^1]))
            {
                _pathPoints.Clear();
                _lineRenderer.positionCount = 0;
            }
            else IsMoving = true;
        }

        if (!player.IsMoving && !player.IsFinish) return;
        if (IsMoving)
            PlayerMoving();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Star"))
        {
            _starCount++;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("BG"))
            transform.gameObject.SetActive(false);

        if (other.TryGetComponent<Player>(out var player))
            transform.gameObject.SetActive(false);
    }

    protected override void PlayerMoving()
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
                IsFinish = true;
                IsMoving = false;
                TryFinishLevel();
            }
        }
    }
}

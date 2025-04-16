using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Sprite> _listSpriteCar = new();
    [SerializeField] protected LineRenderer _lineRenderer;
    [SerializeField] protected CapsuleCollider2D _playerCollider;
    public BoxCollider2D EndPointCollider;
    [SerializeField] protected int _curPointIdx = 0;
    [SerializeField] protected float _moveSpeed = 4f;
    [SerializeField] protected float _rotationSpeed = 10f;
    public bool IsMoving;
    public bool IsFinish;
    [SerializeField] protected int _starCount = 0;
    [SerializeField] protected List<Vector3> _pathPoints;

    public PlayerClone PlayerClone;

    protected virtual void OnEnable()
    {
        IsFinish = false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Star"))
        {
            _starCount++;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("BG"))
            transform.gameObject.SetActive(false);

        if (other.TryGetComponent<PlayerClone>(out var playerClone))
            transform.gameObject.SetActive(false);
    }

    protected virtual void Update()
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

        if (PlayerClone != null && !PlayerClone.IsMoving && !PlayerClone.IsFinish) return;
        if (IsMoving)
            PlayerMoving();
    }

    protected virtual void PlayerMoving()
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
                IsMoving = false;
                IsFinish = true;
                TryFinishLevel();
            }
        }
    }

    public void TryFinishLevel()
    {
        if (IsFinish && PlayerClone != null && PlayerClone.IsFinish)
        {
            LevelManager.Ins.ListLevelStar[LevelManager.Ins.IDLevel] = _starCount;
            _starCount = 0;
            LevelManager.Ins.IDLevel++;
            LevelManager.Ins.ListLevelUnLock[LevelManager.Ins.IDLevel] = true;

            foreach (Transform child in UIManager.Ins.Levels.transform)
            {
                if (child.GetComponent<Player>() == null)
                    child.gameObject.SetActive(false);
            }

            UIManager.Ins.Levels.gameObject.SetActive(false);
            UIManager.Ins.UIPanelLevel.SetActive(true);
        }
    }

    protected virtual void DrawLine()
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

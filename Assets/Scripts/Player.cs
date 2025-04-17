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
    public int StarCount = 0;
    [SerializeField] protected List<Vector3> _pathPoints;

    public PlayerClone PlayerClone;

    private bool _finishTriggered = false;
    public bool _isPathReady = false;

    protected virtual void OnEnable()
    {
        Revive();
    }

    protected virtual void OnDisable()
    {
        PlayerClone = null;
        EndPointCollider = null;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Star"))
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxCollect);
            StarCount++;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("BG"))
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxHit);
            if (PlayerClone != null) PlayerClone.Revive();
            Revive();
            UIManager.Ins.LevelPrefab.BtnAction(LevelManager.Ins.IDLevel);
        }

        if (other.TryGetComponent<PlayerClone>(out var playerClone))
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxHit);
            playerClone.Revive();
            Revive();
            UIManager.Ins.LevelPrefab.BtnAction(LevelManager.Ins.IDLevel);
        }
    }

    public virtual void Revive()
    {
        _pathPoints.Clear();
        _lineRenderer.positionCount = 0;
        IsFinish = false;
        IsMoving = false;
        _finishTriggered = false;
        _isPathReady = false;
        StarCount = 0;
    }
    protected virtual void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0) && !_isPathReady && !IsFinish)
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
            else
            {
                _isPathReady = true;
                TryStartMoving();
            }
        }
#else
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Moved && !_isPathReady && !IsFinish)
        {
            DrawLine();
            _curPointIdx = 0;
        }

        if (touch.phase == TouchPhase.Ended)
        {
            if (_pathPoints.Count < 2 || !EndPointCollider.OverlapPoint(_pathPoints[^1]))
            {
                _pathPoints.Clear();
                _lineRenderer.positionCount = 0;
            }
            else
            {
                _isPathReady = true;
                TryStartMoving();
            }
        }
    }
#endif

        if (IsMoving)
            PlayerMoving();

        if (IsFinish && (PlayerClone == null || PlayerClone.IsFinish) && !_finishTriggered)
        {
            TryFinishLevel();
            _finishTriggered = true;
        }
    }


    protected virtual void TryStartMoving()
    {
        if (PlayerClone == null || PlayerClone.IsPathReady())
        {
            IsMoving = true;
            if (PlayerClone != null)
                PlayerClone.StartMoving();
        }
    }

    public bool IsPathReady() => _isPathReady;

    protected virtual void PlayerMoving()
    {
        if (_pathPoints.Count == 0) return;

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
            }
        }
    }

    public void StartMoving()
    {
        IsMoving = true;
    }

    public void TryFinishLevel()
    {
        LevelManager.Ins.ListLevelStar[LevelManager.Ins.IDLevel] = StarCount;
        StarCount = 0;

        if (LevelManager.Ins.IDLevel < 11)
            LevelManager.Ins.IDLevel++;

        LevelManager.Ins.ListLevelUnLock[LevelManager.Ins.IDLevel] = true;
        foreach (Transform child in UIManager.Ins.Levels.transform)
        {
            if (child.GetComponent<Player>() == null)
                child.gameObject.SetActive(false);
        }
        UIManager.Ins.UIBtnHomeInPlay.SetActive(false);
        UIManager.Ins.Levels.gameObject.SetActive(false);
        UIManager.Ins.UIPanelLevel.SetActive(true);
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
            if (Vector2.Distance(mousePoint2D, _pathPoints[^1]) > 0.1f)
            {
                _pathPoints.Add(mouseWorldPos);
                _lineRenderer.positionCount = _pathPoints.Count;
                _lineRenderer.SetPositions(_pathPoints.ToArray());
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClone : Player
{
    [SerializeField] private Player player;

    protected override void OnEnable()
    {
        base.OnEnable();
        player.PlayerClone = this;
        Revive();
    }

    protected override void Update()
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
    }

    protected override void TryStartMoving()
    {
        if (player.IsPathReady())
        {
            IsMoving = true;
            player.StartMoving();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Star"))
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxBtnClick);
            this.player.StarCount++;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("BG"))
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxHit);
            Revive();
            this.player.Revive();
            UIManager.Ins.LevelPrefab.BtnAction(LevelManager.Ins.IDLevel);
        }

        if (other.TryGetComponent<PlayerClone>(out var playerClone))
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxHit);
            Revive();
            this.player.Revive();
            UIManager.Ins.LevelPrefab.BtnAction(LevelManager.Ins.IDLevel);
        }
    }
    public override void Revive()
    {
        base.Revive();
        if (LevelManager.Ins.IDLevel == 5) transform.SetLocalPositionAndRotation(new Vector3(1f, -3f, 0f), Quaternion.Euler(0, 0, 0));
        else if (LevelManager.Ins.IDLevel == 6) transform.SetLocalPositionAndRotation(new Vector3(0.5f, 3f, 0f), Quaternion.Euler(0, 0, 180));
        else if (LevelManager.Ins.IDLevel == 7) transform.SetLocalPositionAndRotation(new Vector3(1.65f, -3f, 0f), Quaternion.Euler(0, 0, 0));
        else if (LevelManager.Ins.IDLevel == 10) transform.SetLocalPositionAndRotation(new Vector3(1.5f, 3.5f, 0f), Quaternion.Euler(0, 0, 90));
        else if (LevelManager.Ins.IDLevel == 11) transform.SetLocalPositionAndRotation(new Vector3(1.6f, -3.5f, 0f), Quaternion.Euler(0, 0, 0));
    }
}

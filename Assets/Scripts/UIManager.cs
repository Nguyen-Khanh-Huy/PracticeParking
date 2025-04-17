using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Player Player;
    public Levels Levels;
    public GameObject UIPanelMenu;
    public GameObject UIPanelLevel;
    public GameObject UIOnMusic;
    public GameObject UIOffMusic;
    public GameObject UIBtnHomeInPlay;

    public Button BtnPlay;
    public Button BtnOnMusic;
    public Button BtnOffMusic;

    public Button BtnHomeInLevel;

    public Button BtnHomeInPlay;

    public UILevelPrefab LevelPrefab;

    public List<Vector2> ListPlayerPos = new();
    public List<Vector3> ListPlayerRos = new();

    private void Start()
    {
        Levels.gameObject.SetActive(false);
        UIPanelLevel.SetActive(false);
        UIPanelMenu.SetActive(true);
        BtnPlay.onClick.AddListener(() => BtnPlayAction());
        BtnOnMusic.onClick.AddListener(() => BtnOnMusicAction());
        BtnOffMusic.onClick.AddListener(() => BtnOffMusicAction());

        BtnHomeInLevel.onClick.AddListener(() => BtnHomeAction());

        BtnHomeInPlay.onClick.AddListener(() => BtnHomeAction());
    }

    private void BtnPlayAction()
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxBtnClick);
        UIPanelMenu.SetActive(false);
        UIPanelLevel.SetActive(true);
    }

    private void BtnOnMusicAction()
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxBtnClick);
        UIOnMusic.SetActive(false);
        UIOffMusic.SetActive(true);

        if (AudioManager.Ins.AusMusic.isPlaying)
        {
            AudioManager.Ins.AusMusic.Pause();
            AudioManager.Ins.AusSFX.volume = 0f;
        }
        else
        {
            AudioManager.Ins.AusMusic.UnPause();
            AudioManager.Ins.AusSFX.volume = 1f;
        }
    }

    private void BtnOffMusicAction()
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxBtnClick);
        UIOnMusic.SetActive(true);
        UIOffMusic.SetActive(false);

        if (AudioManager.Ins.AusMusic.isPlaying)
        {
            AudioManager.Ins.AusMusic.Pause();
            AudioManager.Ins.AusSFX.volume = 0f;
        }
        else
        {
            AudioManager.Ins.AusMusic.UnPause();
            AudioManager.Ins.AusSFX.volume = 1f;
        }
    }

    private void BtnHomeAction()
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxBtnClick);
        foreach (Transform child in Levels.transform)
        {
            if (child.GetComponent<Player>() == null)
                child.gameObject.SetActive(false);
        }
        Levels.gameObject.SetActive(false);
        UIPanelMenu.SetActive(true);
        UIPanelLevel.SetActive(false);
        UIBtnHomeInPlay.SetActive(false);
    }
}

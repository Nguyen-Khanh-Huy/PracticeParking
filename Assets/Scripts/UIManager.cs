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

    public Button BtnPlay;
    public Button BtnOnMusic;
    public Button BtnOffMusic;

    public Button BtnHomeInLevel;

    public Button BtnRestart;
    public Button BtnHomeInPlay;

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
        UIPanelMenu.SetActive(false);
        UIPanelLevel.SetActive(true);
    }

    private void BtnOnMusicAction()
    {
        UIOnMusic.SetActive(false);
        UIOffMusic.SetActive(true);
        Debug.Log("Off");
    }

    private void BtnOffMusicAction()
    {
        UIOnMusic.SetActive(true);
        UIOffMusic.SetActive(false);
        Debug.Log("On");
    }

    private void BtnHomeAction()
    {
        UIPanelMenu.SetActive(true);
        UIPanelLevel.SetActive(false);
    }
}

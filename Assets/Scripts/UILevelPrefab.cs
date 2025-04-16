using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UILevelPrefab : MonoBehaviour
{
    public Button BtnLv;
    public GameObject ImgLock;
    public GameObject ImgUnLock;
    public GameObject PanelStars;

    [SerializeField] private GameObject _starPassed01;
    [SerializeField] private GameObject _starPassed02;
    [SerializeField] private GameObject _starPassed03;

    public void ShowStar(int star)
    {
        _starPassed01.gameObject.SetActive(star == 1 || star == 2 || star == 3);
        _starPassed02.gameObject.SetActive(star == 2 || star == 3);
        _starPassed03.gameObject.SetActive(star == 3);
    }

    public void BtnAction(int idx)
    {
        if (!LevelManager.Ins.ListLevelUnLock[idx]) return;
        LevelManager.Ins.IDLevel = idx;
        UIManager.Ins.Levels.gameObject.SetActive(true);

        UIManager.Ins.Levels.ListLevels[idx].SetActive(true);
        Transform starsParent = UIManager.Ins.Levels.ListLevels[idx].transform.Find("Stars");
        if (starsParent != null)
        {
            foreach (Transform starChild in starsParent)
            {
                starChild.gameObject.SetActive(true);
            }
        }
        //Transform playerCloneParent = UIManager.Ins.Levels.ListLevels[idx].transform.Find("PlayerClone");
        //if (playerCloneParent != null) playerCloneParent.gameObject.SetActive(true);
        UIManager.Ins.Player.transform.SetPositionAndRotation(UIManager.Ins.ListPlayerPos[idx], Quaternion.Euler(UIManager.Ins.ListPlayerRos[idx]));
        UIManager.Ins.UIBtnHomeInPlay.SetActive(true);

        UIManager.Ins.UIPanelLevel.SetActive(false);
    }
}

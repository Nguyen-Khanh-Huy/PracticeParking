using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelListLevel : MonoBehaviour
{
    [SerializeField] private UILevelPrefab _levelPrefab;
    [SerializeField] private Transform _content;

    private void OnEnable()
    {
        for (int i = 0; i < LevelManager.Ins.ListImg.Count; i++)
        {
            int idx = i;
            UILevelPrefab newPrb = Instantiate(_levelPrefab, _content);
            newPrb.ImgUnLock.GetComponent<Image>().sprite = LevelManager.Ins.ListImg[idx];
            newPrb.ImgLock.SetActive(!LevelManager.Ins.ListLevelUnLock[idx]);
            newPrb.ImgUnLock.SetActive(LevelManager.Ins.ListLevelUnLock[idx]);
            newPrb.PanelStars.SetActive(LevelManager.Ins.ListLevelUnLock[idx]);
            newPrb.ShowStar(LevelManager.Ins.ListLevelStar[idx]);
            newPrb.BtnLv.onClick.AddListener(() => newPrb.BtnAction(idx));
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in _content)
        {
            Destroy(child.gameObject);
        }
    }
}

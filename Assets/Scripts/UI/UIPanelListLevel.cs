using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelListLevel : MonoBehaviour
{
    [SerializeField] private UILevelPrefab _levelPrefab;
    [SerializeField] private Transform _content;
    [SerializeField] private List<Sprite> _listImgLv = new();
    private void OnEnable()
    {
        for(int i = 0; i < _listImgLv.Count; i++)
        {
            UILevelPrefab newPrb = Instantiate(_levelPrefab, _content);
            newPrb.ImgUnLock.GetComponent<Image>().sprite = _listImgLv[i];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public int IDLevel = 0;
    public List<Sprite> ListImg = new();
    public List<bool> ListLevelUnLock = new();
    public List<int> ListLevelStar = new();


    public override void Awake()
    {
        base.Awake();
        IDLevel = 0;
        for (int i = 0; i < ListImg.Count; i++)
        {
            int idx = i;
            if (idx == 0)
            {
                ListLevelUnLock[idx] = true;
            }
            else ListLevelUnLock[idx] = false;
            ListLevelStar[idx] = 0;
        }
    }
}

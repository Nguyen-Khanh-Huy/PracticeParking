using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointClone : MonoBehaviour
{
    [SerializeField] private PlayerClone playerClone;
    [SerializeField] private EndPoint endPoint;
    private void OnEnable()
    {
        Debug.Log(endPoint.IdxRandom);
        GetComponent<SpriteRenderer>().sprite = endPoint.ListSpriteEndPoint[endPoint.IdxRandom];
        playerClone.GetComponent<SpriteRenderer>().sprite = playerClone._listSpriteCar[endPoint.IdxRandom];
        playerClone.EndPointCollider = GetComponent<BoxCollider2D>();
    }
}

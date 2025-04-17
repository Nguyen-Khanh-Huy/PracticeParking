using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<Sprite> _listSpriteEndPoint = new();
    public int IdxRandom;

    public List<Sprite> ListSpriteEndPoint { get => _listSpriteEndPoint; set => _listSpriteEndPoint = value; }

    private void OnEnable()
    {
        IdxRandom = -1;
        int rd = Random.Range(0, _listSpriteEndPoint.Count);
        GetComponent<SpriteRenderer>().sprite = _listSpriteEndPoint[rd];

        int otherRd;
        do
        {
            otherRd = Random.Range(0, _listSpriteEndPoint.Count);
        }
        while (otherRd == rd);

        IdxRandom = otherRd;

        _player.GetComponent<SpriteRenderer>().sprite = _player._listSpriteCar[rd];
        _player.EndPointCollider = GetComponent<BoxCollider2D>();
    }
}

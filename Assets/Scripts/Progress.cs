using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    [SerializeField] private int _coinsCount = 0;

    private bool _isCanDraw;
    private int _maxCoins = 100;
    private int _cost = 5;

    public int CoinsCount => _coinsCount;
    public bool IsCanDraw => _isCanDraw;

    private void Start()
    {
        _isCanDraw = true;
    }

    public void AddCoins(Coin coin)
    {
        _coinsCount += coin.Price;

        if(_coinsCount >= _maxCoins)
        {
            Time.timeScale = 0;
            _isCanDraw = true;
            _maxCoins *= 3;
        }
    }

    public void SendMoney()
    {
        _coinsCount -= _cost;

        if(_coinsCount < _cost)
        {
            _isCanDraw =false;
            Time.timeScale = 1;
            _cost += 1;
        }
    }

    public bool GetIsCanDraw()
    {
        _isCanDraw = false;
        return _isCanDraw;
    }
}

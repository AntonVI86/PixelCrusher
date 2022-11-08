using UnityEngine;
using UnityEngine.Events;

public class Progress : MonoBehaviour
{
    [SerializeField] private int _currentCoins;
    [SerializeField] private int _coinsToNextDrawing;
    [SerializeField] private int _maxCoins;
    [SerializeField] private int _costStepRise = 25;

    public const string TotalScore = "TotalScore";

    public event UnityAction Paused;
    public event UnityAction<Progress> MoneyChanged;
    public event UnityAction Reached;

    private bool _isCanDraw;
    private int _cost = 10;

    public int CoinsCount => _currentCoins;
    public int MaxCoins => _maxCoins;
    public bool IsCanDraw => _isCanDraw;

    private void Start()
    {
        _isCanDraw = true;
        Paused?.Invoke();
        MoneyChanged?.Invoke(this);
    }

    public void AddCoins(Coin coin)
    {
        _currentCoins += coin.Price;
        MoneyChanged?.Invoke(this);

        if(_currentCoins >= _coinsToNextDrawing)
        {
            Paused?.Invoke();
            _isCanDraw = true;
            _coinsToNextDrawing += _maxCoins/3;
        }

        if(_currentCoins >= _maxCoins)
        {
            Reached?.Invoke();
        }
    }

    public void SendMoney()
    {
        _currentCoins -= _cost;
        MoneyChanged?.Invoke(this);

        if(_currentCoins < _cost)
        {
            _isCanDraw = false;
            _cost += _costStepRise;
        }
    }

    public bool GetIsCanDraw()
    {
        _isCanDraw = false;
        return _isCanDraw;
    }
}

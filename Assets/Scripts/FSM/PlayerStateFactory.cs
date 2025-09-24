using System;
using System.Collections.Generic;

public class PlayerStateFactory
{
    private readonly Dictionary<Type, PlayerState> _states = new();
    private readonly PlayerController _player;

    public PlayerStateFactory(PlayerController player)
    {
        _player = player;
    }

    /// <summary>
    /// 요청한 타입의 상태를 반환한다. 없으면 새로 생성 후 캐싱.
    /// </summary>
    public T Get<T>() where T : PlayerState
    {
        var type = typeof(T);

        if (_states.TryGetValue(type, out var state))
        {
            return (T)state;
        }
        
        var newState = (T)Activator.CreateInstance(typeof(T), new object[] { _player });

        _states[type] = newState;
        return newState;
    }
}

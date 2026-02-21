using System;
using System.Collections.Generic;

public static class GameServiceRegistry
{
    private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();
    private static readonly Dictionary<Type, Action<object>> pendingCallbacks = new Dictionary<Type, Action<object>>();
    private static readonly Dictionary<Type, Action<object>> allDemands = new Dictionary<Type, Action<object>>();


    public static void Register<T>(T service)
    {
        var type = typeof(T);
        services[type] = service;

        if (pendingCallbacks.TryGetValue(type, out var callback))
        {
            callback?.Invoke(service);
            pendingCallbacks.Remove(type);
        }
    }

    public static bool Get<T>(Action<T> callbackOnServiceRegistered = null)
    {
        var type = typeof(T);

        if (services.TryGetValue(type, out var registeredService))
        {
            callbackOnServiceRegistered?.Invoke((T)registeredService);
            return true;
        }

        if (callbackOnServiceRegistered != null)
        {
            if (allDemands.ContainsKey(type))
                pendingCallbacks[type] += (obj) => callbackOnServiceRegistered((T)obj);
            else
                allDemands[type] = (obj) => callbackOnServiceRegistered((T)obj);

            if (pendingCallbacks.ContainsKey(type))
                pendingCallbacks[type] += (obj) => callbackOnServiceRegistered((T)obj);
            else
                pendingCallbacks[type] = (obj) => callbackOnServiceRegistered((T)obj);
        }

        return false;
    }



    public static void Unregister<T>()
    {
        var type = typeof(T);

        services.Remove(type);

        if (allDemands.TryGetValue(type, out var callback))
        {
            if (pendingCallbacks.ContainsKey(type))
                pendingCallbacks[type] += (obj) => callback((T)obj);
            else
                pendingCallbacks[type] = (obj) => callback((T)obj);
        }
    }

    public static void ClearAll()
    {
        services.Clear();
        pendingCallbacks.Clear();
    }

    public static bool IsInjected(this object checkObject)
    {
        return checkObject != null && checkObject != default;
    }

}


public interface IGameService
{
    void RegisterServiceToRegistry();
}
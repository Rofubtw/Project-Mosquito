using System;
using System.Reflection;

public static class DependencyInjector
{
    public static void InjectInto(object target)
    {
        var targetType = target.GetType();

        var fields = targetType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        foreach (var field in fields)
        {
            if (!Attribute.IsDefined(field, typeof(InjectAttribute)))
                continue;

            var fieldType = field.FieldType;

            var assignCallbackMethod = new Action<object>((dependency) =>
            {
                field.SetValue(target, dependency);
            });

            var callbackDelegate = Delegate.CreateDelegate(
                typeof(Action<>).MakeGenericType(fieldType),
                assignCallbackMethod.Target,
                assignCallbackMethod.Method
            );

            typeof(GameServiceRegistry)
            .GetMethod("Get", BindingFlags.Public | BindingFlags.Static)
            .MakeGenericMethod(fieldType)
            .Invoke(null, new object[] { callbackDelegate });
        }
    }
}
//Author: Benjamin Bertolli

using System;

public static class ActionExtensions
{

    public static bool InvokeAction<A>(this Action self)
    {

        if (self == null)
        {
            //No subscribers
            return false;

        }

        self();
        return true;

    }
    public static bool InvokeAction<A>(this Action<A> self, A arg1)
    {

        if (self == null)
        {

            //No subscribers
            return false;

        }

        self(arg1);
        return true;

    }
}

public static class Actions
{
    public static Action<int> OnChangeCurrency;
    public static Action<int> OrderCurrencyUpdate;
}


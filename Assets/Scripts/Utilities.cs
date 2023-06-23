using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{

    public static string Monospace<T>(this T original, bool enabled = true)
    {
        return enabled ? $"<mspace=55px>{original}</mspace>" : original.ToString();
    }

}

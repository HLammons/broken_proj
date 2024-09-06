using System;
using System.Collections.Generic;
using UnityEngine;

public struct JsonDateTime
{
    public long value;

    public static implicit operator DateTime(JsonDateTime jdt)
    {
        Debug.Log("Converted to time");
        return DateTime.FromFileTimeUtc(jdt.value);
    }
    public static implicit operator JsonDateTime(DateTime dt)
    {
        Debug.Log("Converted to JDT");
        JsonDateTime jdt = new JsonDateTime();
        jdt.value = dt.ToFileTimeUtc();
        return jdt;
    }
}
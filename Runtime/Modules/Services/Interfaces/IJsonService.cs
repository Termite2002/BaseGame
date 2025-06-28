using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Termite.BaseGame
{
    public interface IJsonService
    {

        string ToJson(object obj);

        T FromJson<T>(string json);

    }
}

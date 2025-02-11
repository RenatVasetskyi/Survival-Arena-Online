using System.Collections;
using UnityEngine;

namespace Business.Architecture.Services.Interfaces
{
    public interface ICoroutineRunner 
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine coroutine);
    }
}

using UnityEngine;

namespace Business.UI.Interfaces
{
    public interface ILoadingCurtain
    {
        GameObject GameObject { get; }
        void Show();
        void Hide();
    }
}
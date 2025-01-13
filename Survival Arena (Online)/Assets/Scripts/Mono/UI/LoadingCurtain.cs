using Business.UI.Interfaces;
using UnityEngine;

namespace Mono.UI
{
    public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        private const int StartValue = 1;
        private const int EndValue = 0;
        
        private const float Duration = 0.5f;
        
        [SerializeField] private CanvasGroup _canvasGroup;

        private bool _isHiding;

        public GameObject GameObject => gameObject;

        public void Show()
        {
            _canvasGroup.alpha = StartValue;
            
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            if (_isHiding)
                return;
            
            _isHiding = true;
            
            _canvasGroup.alpha = StartValue;
            
            LeanTween.value(StartValue, EndValue, Duration)
                .setOnUpdate((value) => _canvasGroup.alpha = value)
                .setOnComplete(() => Destroy(gameObject));
        }
    }
}
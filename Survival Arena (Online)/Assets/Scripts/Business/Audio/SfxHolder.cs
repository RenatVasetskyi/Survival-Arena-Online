using System.Collections.Generic;
using UnityEngine;

namespace Business.Audio
{
    [CreateAssetMenu(fileName = "SfxHolder", menuName = "Create Sfx Holder/Holder")]
    public class SfxHolder : ScriptableObject
    {
        [SerializeField] private List<SfxData> _soundEffects;
        public List<SfxData> SoundEffects => _soundEffects;
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Business.Audio
{
    [CreateAssetMenu(fileName = "MusicHolder", menuName = "Create Music Holder/Holder")]
    public class MusicHolder : ScriptableObject
    {
        [SerializeField] private List<MusicData> _musics;
        public List<MusicData> Musics => _musics;
    }
}
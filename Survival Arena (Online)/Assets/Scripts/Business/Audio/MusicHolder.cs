using System.Collections.Generic;
using UnityEngine;

namespace Business.Audio
{
    [CreateAssetMenu(fileName = "MusicHolder", menuName = "Create Music Holder/Holder")]
    public class MusicHolder : ScriptableObject
    {
        public List<MusicData> Musics;
    }
}
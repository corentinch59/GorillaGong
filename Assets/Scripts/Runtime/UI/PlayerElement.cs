using GorillaGong.Runtime.Player;
using UnityEngine;

namespace GorillaGong.Runtime.UI
{
    public abstract class PlayerElement : MonoBehaviour
    {
        public abstract void Deploy(IPlayerModel player);
    }
}

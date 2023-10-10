using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class PlayerElement : MonoBehaviour
    {
        public abstract void Deploy(IPlayerModel player);
    }
}

using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public abstract class ReadOnlyPlayerModelList : ScriptableObject
{
    public abstract IReadOnlyList<IPlayerModel> PlayerModels { get; }
}

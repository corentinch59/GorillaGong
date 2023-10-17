using System.Collections;
using System.Collections.Generic;
using GorillaGong.Runtime;
using NaughtyAttributes;
using UnityEngine;
using TMPro;

namespace Runtime.UI
{
    public class StartGameTimer : FloatVariableListener
    {
        [SerializeField] private TextMeshProUGUI _text;

        public override void OnVariableValueChanged()
        {
            if(_variable.Value >= 0.9f)
                base.OnVariableValueChanged();
            _text.text = _variable.Value.ToString("#");
            if(_variable.Value < 1)
                _text.text = "GO";
        }
    }
}

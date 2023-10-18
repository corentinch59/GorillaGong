using System.Collections;
using System.Collections.Generic;
using GorillaGong.Runtime;
using UnityEngine;
using TMPro;

namespace Runtime.UI
{
    public class TimerBinder : FloatVariableListener
    {
        [SerializeField] private TextMeshProUGUI _text;

        public override void OnVariableValueChanged()
        {
            if (_variable.Value >= 0.5f)
                base.OnVariableValueChanged();
            _text.text = _variable.Value.ToString("#");
            if (_variable.Value < 1)
            {
                _text.text = "0";
            }
        }
    }
}

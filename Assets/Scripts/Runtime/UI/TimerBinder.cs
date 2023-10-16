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
            _text.text = _variable.Value.ToString("F1");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using GorillaGong.Runtime.Variables;
using UnityEngine;
using TMPro;

namespace Runtime.UI
{
    public class EventTitleBinder : MonoBehaviour
    {
        [SerializeField] private GameModeConfigVariable _config;
        [SerializeField] private TextMeshProUGUI _text;

        public void BindTitle()
        {
            _text.text = "Event Incoming : " + _config.Value.Title;
        }
    }

}

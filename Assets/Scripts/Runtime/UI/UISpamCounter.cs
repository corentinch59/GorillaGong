using GorillaGong.Runtime.RuntimeSets.Listeners;
using MoreMountains.Feedbacks;
using UniRx;
using UnityEngine;
using TMPro;

namespace Runtime.UI
{
    public class UISpamCounter : RuntimeSetListener<int>
    {
        [SerializeField] private int _playerIndex;
        [SerializeField] private TextMeshProUGUI _playerHitCountText;
        [SerializeField] private MMF_Player _feedback;

        protected override void OnValueChanged(CollectionReplaceEvent<int> collectionReplaceEvent)
        {
            if (collectionReplaceEvent.Index != _playerIndex)
                return;

            _playerHitCountText.text = collectionReplaceEvent.NewValue.ToString();
            _feedback.PlayFeedbacks();
        }
    }
}


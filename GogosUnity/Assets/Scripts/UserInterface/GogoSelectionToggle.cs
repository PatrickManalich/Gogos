using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gogos
{
    public class GogoSelectionToggle : MonoBehaviour, ISelectHandler
    {
        public event EventHandler<GogoSelectedEventArgs> GogoSelected;

        public Toggle Toggle => m_Toggle;

        public AbstractScriptableGogo ScriptableGogo { get; private set; }

        [SerializeField]
        private Toggle m_Toggle;

        [SerializeField]
        private NicknameText m_NicknameText;

        [SerializeField]
        private GogoVariantIcon m_GogoVariantIcon;

        [SerializeField]
        private Portrait m_Portrait;

        public void SetToggle(AbstractScriptableGogo scriptableGogo)
        {
            ScriptableGogo = scriptableGogo;

            m_NicknameText.SetNickname(scriptableGogo.Nickname, scriptableGogo.RarityTier);
            m_GogoVariantIcon.SetIcon(scriptableGogo.GogoVariant);
            m_Portrait.SetPortrait(scriptableGogo.Portrait);
        }

        public void OnSelect(BaseEventData eventData)
        {
            Toggle.isOn = true;
            GogoSelected?.Invoke(this, new GogoSelectedEventArgs(ScriptableGogo));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace MinigameTwo
{
    [RequireComponent(typeof(TMP_Text))]
    public class LinkHandlerForTMPText : MonoBehaviour
    {
        private TMP_Text _tmpTextBox;
        private Canvas _canvasToCheck;
        [SerializeField] private Camera cameraToUse;

        public delegate void ClickOnLinkEvent(string keyword);
        public static event ClickOnLinkEvent OnClickedOnLinkEvent;

        private void Awake()
        {
            _tmpTextBox = GetComponent<TMP_Text>();
            _canvasToCheck = GetComponentInParent<Canvas>();

            if (_canvasToCheck.renderMode == RenderMode.ScreenSpaceOverlay)
                cameraToUse = null;
            else
                cameraToUse = _canvasToCheck.worldCamera;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Vector3 mousePosition = new Vector3(eventData.position.x, eventData.position.y, 0);

            var linkTaggedText = TMP_TextUtilities.FindIntersectingLink(_tmpTextBox, mousePosition, cameraToUse);

            if (linkTaggedText != -1)
            {
                TMP_LinkInfo linkInfo = _tmpTextBox.textInfo.linkInfo[linkTaggedText];

                OnClickedOnLinkEvent?.Invoke(linkInfo.GetLinkText());
            }
        }
    }
}

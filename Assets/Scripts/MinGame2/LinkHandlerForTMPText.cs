using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace MinigameTwo
{
    [RequireComponent(typeof(TMP_Text))]
    public class LinkHandlerForTMPText : MonoBehaviour, IPointerClickHandler
    {
        private TMP_Text _tmpTextBox;
        private Canvas _canvasToCheck;
        [SerializeField] private Camera cameraToUse;
        [SerializeField] private GameObject answerBox;
        [SerializeField] private GameObject popup;
        [SerializeField] private GameObject _masterMenu;

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

                QuestionFormats request = new QuestionFormats();
                request.index = linkInfo.linkTextfirstCharacterIndex;
                request.word = linkInfo.GetLinkText();

                int indexWTags = 0;
                string boxText = _tmpTextBox.GetComponent<TMP_Text>().GetParsedText();
                string soFar = boxText.Substring(0, request.index);

                QuestionFormats tempQF = new QuestionFormats();
                tempQF = _masterMenu.GetComponent<Minigame2Manager>().clickableWords[(soFar.Split(" ").Length - 1)];

                indexWTags += 9 + request.word.Length +
                    soFar.Length + soFar.Replace(" ", "").Length +
                    (16 * (soFar.Split(" ").Length - 1));
                request.indexWithTags = indexWTags;

                if (tempQF.mistake)
                {
                    popup.GetComponent<PopupMenu>().setYesButton(true);
                    popup.GetComponent<PopupMenu>().setNoButton(true);
                    popup.GetComponent<PopupMenu>().setBadYesButton(false);
                }
                else
                {
                    popup.GetComponent<PopupMenu>().setYesButton(false);
                    popup.GetComponent<PopupMenu>().setNoButton(true);
                    popup.GetComponent<PopupMenu>().setBadYesButton(true);
                }
                request.correctAnswer = tempQF.correctAnswer;

                float popupw = popup.GetComponent<RectTransform>().rect.width / 2.0f;
                float popuph = popup.GetComponent<RectTransform>().rect.height / 2.0f;

                float upperBounds = Screen.height - popuph;
                float lowerBounds = 0 + popuph;

                float leftBounds = 0 + popupw;
                float rightBounds = Screen.width - popupw;

                float popupx = Math.Min(Math.Max((mousePosition.x + popupw), leftBounds), rightBounds);
                float popupy = Math.Min(Math.Max((mousePosition.y - popuph), lowerBounds), upperBounds);

                popup.transform.position = new Vector3(
                    popupx,
                    popupy,
                    0);
                answerBox.GetComponent<AnswerBlank>().setCurQues(request);
                OnClickedOnLinkEvent?.Invoke(linkInfo.GetLinkText());
                
            }
        }
    }
}

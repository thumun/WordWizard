using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MinigameTwo
{
    public class TooltipHandler : MonoBehaviour
    {
        [SerializeField] private List<QuestionFormats> questionContentList;

        [SerializeField] private GameObject tooltipContainer;
        // private TMP_Text _tooltipDescriptionTMP;
        // [SerializeField] private Image iconDisplay;

        public QuestionFormats curQues;

        public void setCurQues(QuestionFormats cQ)
        {
            curQues = cQ;
        }

        private void Awake()
        {
            // _tooltipDescriptionTMP = tooltipContainer.GetComponentInChildren<TMP_Text>();
        }

        private void OnEnable()
        {
            LinkHandlerForTMPText.OnClickedOnLinkEvent += GetTooltipInfo;
        }

        private void OnDisable()
        {
            LinkHandlerForTMPText.OnClickedOnLinkEvent -= GetTooltipInfo;
        }

        private void GetTooltipInfo(string keyword)
        {
            foreach (var entry in questionContentList)
            {
                if (entry.Keyword == keyword)
                {
                    // if (!tooltipContainer.gameObject.activeInHierarchy)
                    // tooltipContainer.gameObject.transform.position = mousePosition;
                    tooltipContainer.gameObject.SetActive(true);
                    // Debug.Log(entry.word);
                    // _tooltipDescriptionTMP.text = entry.Keyword;
                    // iconDisplay.sprite = entry.Image;

                    return;
                }
            }
        }

        public void CloseTooltip()
        {
            if (tooltipContainer.gameObject.activeInHierarchy)
            {
                tooltipContainer.SetActive(false);
            }
        }

        public void AddtoQuestions(QuestionFormats q)
        {
            questionContentList.Add(q);
        }

    }
}
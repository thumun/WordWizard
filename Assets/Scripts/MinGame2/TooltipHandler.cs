using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MinigameTwo
{
    public class TooltipHandler : MonoBehaviour
    {
        [SerializeField] private List<TooltipInfos> tooltipContentList;

        [SerializeField] private GameObject tooltipContainer;
        private TMP_Text _tooltipDescriptionTMP;
        [SerializeField] private Image iconDisplay;

        private void Awake()
        {
            _tooltipDescriptionTMP = tooltipContainer.GetComponentInChildren<TMP_Text>();
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
            foreach (var entry in tooltipContentList)
            {
                if (entry.Keyword == keyword)
                {
                    if (!tooltipContainer.gameObject.activeInHierarchy)
                        tooltipContainer.gameObject.SetActive(true);

                    _tooltipDescriptionTMP.text = entry.Keyword;
                    iconDisplay.sprite = entry.Image;
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

    }
}
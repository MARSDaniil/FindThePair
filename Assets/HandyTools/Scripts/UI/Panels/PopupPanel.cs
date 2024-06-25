using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace HandyTools {
    public class PopupPanel : Panel {
        #region Fields

        [SerializeField] protected CanvasGroup InnerCGroup; //for disabling buttons before thy fully appear
        [SerializeField] protected RectTransform ScalableRect;
        [SerializeField] protected Image Background;
        public Button CloseButton;
        [SerializeField] protected bool DeleteOnClose = true;
        [SerializeField] protected float BackgroundAlpha = 0.82f;

        #endregion

        protected override bool IsDeleteOnClose => DeleteOnClose;
    
        void Start() {
            if (CloseButton != null) {
                CloseButton.onClick.AddListener(Close);
            }
        }

        void OnDestroy() {
            ScalableRect?.DOKill();
            Background?.DOKill();
        }

       
        #region Panel
        protected override void OnShowStart() {
            this.gameObject.SetActive(true);

            if (InnerCGroup != null) InnerCGroup.blocksRaycasts = false;

            //Bcgr fade in
            if (Background) {
                float FinalAlpha = BackgroundAlpha;
                Color StartColor = Background.color;
                StartColor.a = 0.0f;
                Background.color = StartColor;
                Background.DOFade(FinalAlpha, 0.3f).SetUpdate(true); ;
            }

            if (ScalableRect != null) {
                ScalableRect.localScale = Vector3.one;
                ScalableRect.DOPunchScale(new Vector3(0.2f, 0.2f), 0.5f, 3).OnComplete(OnShowFinish).SetUpdate(true);
            } else {
                Invoke("OnShowFinish", 0.5f);
            }

            //PlaySound();

            base.OnShowStart();
        }

        protected override void OnShowFinish() {
            if (InnerCGroup != null) InnerCGroup.blocksRaycasts = true;

            base.OnShowFinish();
        }

        protected override void OnCloseStart() {
            if (InnerCGroup != null) InnerCGroup.blocksRaycasts = false;

            ScalableRect?.DOKill();
            Background?.DOFade(0, 0.6f).OnComplete(OnCloseFinish).SetUpdate(true); ;
            ScalableRect?.DOScale(new Vector3(0.0f, 0.0f, 1.0f), 0.4f).SetEase(Ease.InBack).SetUpdate(true); ;

            base.OnCloseStart();
        }

        protected override void OnCloseFinish() {
            base.OnCloseFinish();
            if (!DeleteOnClose) {
                gameObject.SetActive(false);
            }
            else {
                Destroy(gameObject);
            }
        }

        #endregion
    } 
}
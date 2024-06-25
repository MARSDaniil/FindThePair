using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HandyTools {
    public interface IPanel {
        void Show();
        void Close();
        GameObject GetGameObject();
        bool IsShowing();
        bool IsClosing();
        bool IsCanCloseByBack { get; }
        void InitCloseCallbacksInUIManager(
            UnityAction<IPanel> CloseStartCallback,
            UnityAction<IPanel, bool> CloseFinishCallback);
    }
    public class Panel : MonoBehaviour, IPanel {
        #region Fields
        protected UnityAction<IPanel> PanelCloseStartCallback;//Panel
        protected UnityAction<IPanel, bool> PanelCloseFinishCallback;//Panel, IsDeleteOnClose
        protected bool IsShowingAnim;
        protected bool IsClosingAnim;
        protected bool IsPanelShown;
        #endregion

        protected virtual bool IsDeleteOnClose => false; // for overrides


        #region IPanel
        public virtual void Show() {
            if (IsShowingAnim || IsClosingAnim) return;
            IsShowingAnim = true;
            IsPanelShown = true;
            OnShowStart();
        }

        public virtual void Close() {
            if (IsShowingAnim || IsClosingAnim) return;
            IsClosingAnim = true;
            OnCloseStart();
        }

        public GameObject GetGameObject() {
            return this.gameObject;
        }

        public bool IsShowing() {
            return IsShowingAnim;
        }

        public bool IsClosing() {
            return IsClosingAnim;
        }

        public bool IsCanCloseByBack { get; }
        public void InitCloseCallbacksInUIManager(UnityAction<IPanel> CloseStartCallback, UnityAction<IPanel, bool> CloseFinishCallback) {
            this.PanelCloseStartCallback = CloseStartCallback;
            this.PanelCloseFinishCallback = CloseFinishCallback;
        }

        #endregion


        #region Helpers
        protected virtual void OnShowStart() {
        }

        protected virtual void OnShowFinish() {
            IsShowingAnim = false;
        }

        protected virtual void OnCloseStart() {
            if (PanelCloseStartCallback != null) {
                PanelCloseStartCallback(this);
            }
        }

        protected virtual void OnCloseFinish() {
            IsClosingAnim = false;
            IsPanelShown = false;
            if (PanelCloseFinishCallback != null) {
                PanelCloseFinishCallback(this, IsDeleteOnClose);
            }
        }
        #endregion
    }
}

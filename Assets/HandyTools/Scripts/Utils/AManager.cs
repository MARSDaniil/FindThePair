using System;
using UnityEngine;
using System.Collections.Generic;

namespace HandyTools {

    public class AManager<T> : MonoBehaviour where T : AManager<T> {

        public delegate void InitComplete();

        public static event InitComplete e_onInitComplete;

        #region Fields
        protected bool m_initialized = false;

        virtual protected bool DontDOnLoad { get { return false; } }
        #endregion

        #region Static
        protected static T m_instance;

        public static bool Exist {
            get { return (m_instance != null); }
        }

        public static T I {
            get {
                if (m_instance == null)
                    Create();
                return m_instance;
            }
        }

        protected static void Create() {
            GameObject go = new GameObject();
            go.name = typeof(T).Name;
            m_instance = go.AddComponent(typeof(T)) as T;
        }

        public static void Destroy(bool a_immediate = false) {
            if (m_instance != null && m_instance.gameObject != null) {
                if (a_immediate) {
                    DestroyImmediate(m_instance.gameObject);
                } else {
                    GameObject.Destroy(m_instance.gameObject);
                }
            }
            m_instance = null;
        }
        #endregion

        #region Unity Event Functions
        void Awake() {
            if (m_instance == null) {
                if (!m_initialized)
                    InternalInit();
                m_instance = this as T;
            } else {
                if (m_instance != this) {
                    GameObject.Destroy(gameObject);
                    Debug.LogError($"Two singletons at a time!", m_instance.gameObject);
                }
            }
        }

        private void OnDestroy() {
            OnDestroyRoutine();
        }

        #endregion
        
        protected void InternalInit() {
            if (DontDOnLoad) {
                GameObject.DontDestroyOnLoad(this.gameObject);
            }
            Init();
            m_initialized = true;
            e_onInitComplete?.Invoke();
        }

        #region Virtual Functions
        virtual protected void OnDestroyRoutine() { }

        virtual protected void Init() {
            // Custom Init Code goes here - Do not call it directly...
        }
        #endregion

    }

}


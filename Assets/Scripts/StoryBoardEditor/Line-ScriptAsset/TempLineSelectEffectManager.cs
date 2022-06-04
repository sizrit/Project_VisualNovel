using UnityEngine;

namespace StoryBoardEditor.Line_ScriptAsset
{
    public class TempLineSelectEffectManager : MonoBehaviour
    {
        #region Singleton

        private static TempLineSelectEffectManager _instance;

        public static TempLineSelectEffectManager GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<TempLineSelectEffectManager>();
                if (obj == null)
                {
                    Debug.LogError("TempLineSelectEffectManager Script is not available!");
                    return null;
                }

                _instance = obj;
            }

            return _instance;
        }

        #endregion

        [SerializeField] private Material selectEffect;
        [SerializeField] private Material normal;

        public void ShowEffect(TempLine line)
        {
            line.lineRenderer.material = selectEffect;
        }

        public void RemoveEffect(TempLine line)
        {
            line.lineRenderer.material = normal;
        }
    }
}
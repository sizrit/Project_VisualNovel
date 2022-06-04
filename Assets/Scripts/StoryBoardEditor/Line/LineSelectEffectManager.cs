using UnityEngine;

namespace StoryBoardEditor.Line
{
    public class LineSelectEffectManager : MonoBehaviour
    {
        #region Singleton

        private static LineSelectEffectManager _instance;

        public static LineSelectEffectManager GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<LineSelectEffectManager>();
                if (obj == null)
                {
                    Debug.LogError("LineSelectEffectManager Script is not available!");
                    return null;
                }

                _instance = obj;
            }

            return _instance;
        }

        #endregion
        
        [SerializeField] private Material selectEffect;
        [SerializeField] private Material normal;
        
        public void ShowEffect(StoryBoardEditor.Line.Line line)
        {
            line.lineRenderer.material = selectEffect;
        }

        public void RemoveEffect(StoryBoardEditor.Line.Line line)
        {
            line.lineRenderer.material = normal;
        }
    }
}

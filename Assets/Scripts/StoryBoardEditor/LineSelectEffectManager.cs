using UnityEngine;

namespace StoryBoardEditor
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
        
        [SerializeField] private GameObject effectPrefab;
        [SerializeField] private GameObject effectGameObject;

        public void ShowEffect(Line line)
        {
            if (effectGameObject == null)
            {
                effectGameObject = Instantiate(effectPrefab, this.transform);
                effectGameObject.GetComponent<LineRenderer>().SetPosition(0,line.lineRenderer.GetPosition(0));
                effectGameObject.GetComponent<LineRenderer>().SetPosition(1,line.lineRenderer.GetPosition(1));
            }
        }
        
        public void MoveEffect(Line line)
        {
            effectGameObject.GetComponent<LineRenderer>().SetPosition(0,line.lineRenderer.GetPosition(0));
            effectGameObject.GetComponent<LineRenderer>().SetPosition(1,line.lineRenderer.GetPosition(1));
        }
        
        public void MoveEffect(TempLine line)
        {
            effectGameObject.GetComponent<LineRenderer>().SetPosition(0,line.lineRenderer.GetPosition(0));
            effectGameObject.GetComponent<LineRenderer>().SetPosition(1,line.lineRenderer.GetPosition(1));
        }

        public void RemoveEffect()
        {
            if (effectGameObject == null) return;
            Destroy(effectGameObject);
            effectGameObject = null;
        }

    }
}

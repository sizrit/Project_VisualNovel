using UnityEngine;

namespace StoryBoardEditor
{
    public class NodeSelectEffectManager : MonoBehaviour
    {
        #region Singleton

        private static NodeSelectEffectManager _instance;

        public static NodeSelectEffectManager GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<NodeSelectEffectManager>();
                if (obj == null)
                {
                    Debug.LogError("TempLineManager Script is not available!");
                    return null;
                }

                _instance = obj;
            }

            return _instance;
        }

        #endregion
        
        [SerializeField] private GameObject effectPrefab;
        [SerializeField] private GameObject effectGameObject;

        public void ShowEffect(Node node)
        {
            if (effectGameObject == null)
            {
                effectGameObject = Instantiate(effectPrefab, this.transform);
                effectGameObject.transform.position = node.gameObject.transform.position;
            }
        }

        public void MoveEffect(Vector3 position)
        {
            effectGameObject.transform.position = position;
        }

        public void RemoveEffect()
        {
            if (effectGameObject == null) return;
            Destroy(effectGameObject);
            effectGameObject = null;
        }
    
    }
}

using UnityEngine;

namespace StoryBoardEditor
{
    public class LineManipulator : MonoBehaviour
    {
        #region Singleton

        private static LineManipulator _instance;

        public static LineManipulator GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<LineManipulator>();
                if (obj == null)
                {
                    Debug.LogError("LineManipulator Script is not available!");
                    return null;
                }

                _instance = obj;
            }

            return _instance;
        }

        #endregion

        private Line selectedLine;

        public void SetSelectedLine(GameObject lineGameObject)
        {
            selectedLine = LineManager.GetInstance().GetLine(lineGameObject);
            LineSelectEffectManager.GetInstance().ShowEffect(selectedLine);
        }
        
        public void ClearSelectedLine()
        {
            if (selectedLine != null)
            {
                LineSelectEffectManager.GetInstance().RemoveEffect(selectedLine);
            }
            selectedLine = null;
        }

        public Line GetSelectedLine()
        {
            return selectedLine;
        }

        public void UpdateMeshCollider(Line line)
        {
            Mesh newMesh = new Mesh();
            line.lineRenderer.BakeMesh(newMesh);
            line.gameObject.GetComponent<MeshCollider>().sharedMesh = newMesh;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace StoryBoardEditor
{
    public class StoryBoardEditorNodeCollisionManager
    {
        #region Singleton

        private static StoryBoardEditorNodeCollisionManager instance;

        public static StoryBoardEditorNodeCollisionManager GetInstance()
        {
            if (instance == null)
            {
                instance = new StoryBoardEditorNodeCollisionManager();
            }
            return instance;
        }

        #endregion
        
        private const float Offset = 0.1f;
        private const float Width = 2f;
        private const float Height = 2.5f;
        private const float Delta = 0.2f;
        
        private Vector3 _savedPosition =Vector3.zero;

        public void CheckCollision(GameObject nodeGameObject)
        {
            List<GameObject> nodeList = StoryBoardEditorNodeManager.GetInstance().GetAllNodeGameObject();
        
            Vector3 currentPosition = nodeGameObject.transform.position;
            List<Vector3> allNodePositionList = new List<Vector3>();
            foreach (var node in nodeList)
            {
                if (nodeGameObject != node)
                {
                    allNodePositionList.Add(node.transform.position);
                }
            }

            float dx1 = currentPosition.x - Width - Offset;
            float dx2 = currentPosition.x + Width + Offset;
            float dy1 = currentPosition.y - Height - Offset;
            float dy2 = currentPosition.y + Height + Offset;

            int collisionCount = 0;
            List<Vector3> collisionPositionList = new List<Vector3>();
            foreach (var position in allNodePositionList)
            {
                if (dx1 < position.x && position.x < dx2 && dy1 < position.y && position.y < dy2)
                {
                    collisionCount++;
                    collisionPositionList.Add(position);
                }
            }

            if (collisionCount == 1)
            {
                AdjustNodePosition(nodeGameObject, collisionPositionList[0]);
            }
        }

        private void AdjustNodePosition(GameObject targetNode, Vector3 benchmark)
        {
            Vector3 newPosition = Vector3.zero;
            Vector3 delta = targetNode.transform.position - benchmark;

            if (Width - Mathf.Abs(delta.x) < Height - Mathf.Abs(delta.y))
            {
                if (delta.x > 0)
                {
                    newPosition.x = benchmark.x + Width + Delta;
                }
                else
                {
                    newPosition.x = benchmark.x - Width - Delta;
                }
                newPosition.y = targetNode.transform.position.y;
            }
            else
            {
                if (delta.y> 0)
                {
                    newPosition.y = benchmark.y + Height + Delta;
                }
                else
                {
                    newPosition.y = benchmark.y - Height - Delta;
                }
                newPosition.x = targetNode.transform.position.x;
            }
            targetNode.transform.position = newPosition;
        }
        
    }
}

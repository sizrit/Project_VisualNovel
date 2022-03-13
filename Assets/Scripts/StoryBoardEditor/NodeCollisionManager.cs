using System;
using System.Collections.Generic;
using UnityEngine;

namespace StoryBoardEditor
{
    public class NodeCollisionManager
    {
        #region Singleton

        private static NodeCollisionManager _instance;

        public static NodeCollisionManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new NodeCollisionManager();
            }
            return _instance;
        }

        #endregion
        
        private const float Offset = 0.1f;
        private const float Width = 2f;
        private const float Height = 2.5f;
        private const float Delta = 0.2f;
        
        private Vector3 _savedPosition =Vector3.zero;

        public void CheckCollision(GameObject nodeGameObject, Vector3 backUpPosition)
        {
            List<GameObject> nodeList = NodeManager.GetInstance().GetAllNodeGameObject();
            
            List<Vector3> allNodePositionList = new List<Vector3>();
            foreach (var node in nodeList)
            {
                if (nodeGameObject != node)
                {
                    allNodePositionList.Add(node.transform.position);
                }
            }

            Vector3 currentPosition = nodeGameObject.transform.position;
            float dx1 = currentPosition.x - Width - Offset;
            float dx2 = currentPosition.x + Width + Offset;
            float dy1 = currentPosition.y - Height - Offset;
            float dy2 = currentPosition.y + Height + Offset;
            
            List<Vector3> collisionPositionList = new List<Vector3>();
            foreach (var position in allNodePositionList)
            {
                if (dx1 < position.x && position.x < dx2 && dy1 < position.y && position.y < dy2)
                {
                    collisionPositionList.Add(position);
                }
            }

            if (collisionPositionList.Count == 1)
            {
                AdjustNodePosition(nodeGameObject, collisionPositionList[0]);
            }

            if (collisionPositionList.Count == 2)
            {
                AdjustNodePosition2(nodeGameObject, collisionPositionList[0],collisionPositionList[1]);
            }

            if (collisionPositionList.Count > 2)
            {
                nodeGameObject.transform.position = backUpPosition;
            }

            collisionPositionList.Clear();
            
            Vector3 newPosition = nodeGameObject.transform.position;
            dx1 = newPosition.x - Width - Offset;
            dx2 = newPosition.x + Width + Offset;
            dy1 = newPosition.y - Height - Offset;
            dy2 = newPosition.y + Height + Offset;
            
            foreach (var position in allNodePositionList)
            {
                if (dx1 < position.x && position.x < dx2 && dy1 < position.y && position.y < dy2)
                {
                    collisionPositionList.Add(position);
                }
            }
            
            if (collisionPositionList.Count>0)
            {
                nodeGameObject.transform.position = backUpPosition;
            }
        }

        private void AdjustNodePosition2(GameObject targetNode, Vector3 collisionNode01, Vector3 collisionNode02)
        {
            var targetNodePosition = targetNode.transform.position;

            float dx1 = targetNodePosition.x - collisionNode01.x;
            float dy1 = targetNodePosition.y - collisionNode01.y;
            float dx2 = targetNodePosition.x - collisionNode02.x;
            float dy2 = targetNodePosition.y - collisionNode02.y;

            bool dx1Sign = dx1 > 0;
            bool dy1Sign = dy1 > 0;
            bool dx2Sign = dx2 > 0;
            bool dy2Sign = dy2 > 0;

            dx1 = Math.Abs(dx1);
            dy1 = Math.Abs(dy1);
            dx2 = Math.Abs(dx2);
            dy2 = Math.Abs(dy2);
            
            float collisionNode01HorizontalScalar = Width + Delta - dx1;
            float collisionNode01VerticalScalar = Height + Delta - dy1;
            float collisionNode02HorizontalScalar = Width + Delta - dx2;
            float collisionNode02VerticalScalar = Height + Delta - dy2;

            if (dx1Sign != dx2Sign && dy1Sign == dy2Sign)
            {
                if (collisionNode01VerticalScalar > collisionNode02VerticalScalar)
                {
                    targetNode.transform.position +=
                        new Vector3(collisionNode01HorizontalScalar * (dx1Sign ? 1 : -1),
                            collisionNode02VerticalScalar * (dy2Sign ? 1 : -1), 0);
                }
                else
                {
                    targetNode.transform.position +=
                        new Vector3(collisionNode02HorizontalScalar * (dx2Sign ? 1 : -1),
                            collisionNode01VerticalScalar * (dy1Sign ? 1 : -1), 0);
                }
            }

            if (dx1Sign == dx2Sign && dy1Sign != dy2Sign)
            {
                if (collisionNode01HorizontalScalar > collisionNode02HorizontalScalar)
                {
                    targetNode.transform.position +=
                        new Vector3(collisionNode02HorizontalScalar * (dx2Sign ? 1 : -1),
                            collisionNode01VerticalScalar * (dy1Sign ? 1 : -1), 0);
                }
                else
                {
                    targetNode.transform.position +=
                        new Vector3(collisionNode01HorizontalScalar * (dx1Sign ? 1 : -1),
                            collisionNode02VerticalScalar * (dy2Sign ? 1 : -1), 0);
                }
            }

            if (dx1Sign != dx2Sign && dy1Sign != dy2Sign)
            {
                float set1 = collisionNode01HorizontalScalar + collisionNode02VerticalScalar;
                float set2 = collisionNode02HorizontalScalar + collisionNode01VerticalScalar;

                if (set1 > set2)
                {
                    targetNode.transform.position +=
                        new Vector3(collisionNode02HorizontalScalar * (dx2Sign ? 1 : -1),
                            collisionNode01VerticalScalar * (dy1Sign ? 1 : -1), 0);
                }
                else
                {
                    targetNode.transform.position +=
                        new Vector3(collisionNode01HorizontalScalar * (dx1Sign ? 1 : -1),
                            collisionNode02VerticalScalar * (dy2Sign ? 1 : -1), 0);
                }
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

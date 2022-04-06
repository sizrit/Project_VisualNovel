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
        
        private float offset = 0.1f;
        // private float width = 2f;
        // private float height = 2.5f;
        private float delta = 0.2f;
        
        private Vector3 _savedPosition =Vector3.zero;

        public void CheckCollision(GameObject nodeGameObject, Vector3 backUpPosition)
        {
            List<GameObject> collisionNodeList = GetCollisionNodeList(nodeGameObject);

            if (collisionNodeList.Count == 1)
            {
                AdjustNodePosition(nodeGameObject, collisionPositionList[0]);
            }

            if (collisionPositionList.Count == 2)
            {
                AdjustNodePosition2(nodeGameObject, collisionPositionList[0],collisionPositionList[1]);
            }

            if (collisionPositionList.Count > 2)
            {
                NodeManipulator.GetInstance()
                    .MoveNodePosition(NodeManager.GetInstance().GetNodeByGameObject(nodeGameObject), backUpPosition);
            }

            collisionPositionList.Clear();
            
            // Vector3 newPosition = nodeGameObject.transform.position;
            // dx1 = newPosition.x - width - offset;
            // dx2 = newPosition.x + width + offset;
            // dy1 = newPosition.y - height - offset;
            // dy2 = newPosition.y + height + offset;
            
            foreach (var position in allNodePositionList)
            {
                if (dx1 < position.x && position.x < dx2 && dy1 < position.y && position.y < dy2)
                {
                    collisionPositionList.Add(position);
                }
            }
            
            if (collisionPositionList.Count>0)
            {
                NodeManipulator.GetInstance()
                    .MoveNodePosition(NodeManager.GetInstance().GetNodeByGameObject(nodeGameObject), backUpPosition);
            }
        }

        private List<GameObject> GetCollisionNodeList(GameObject nodeGameObject)
        {
            List<GameObject> collisionNodeGameObjects = new List<GameObject>();
            
            List<GameObject> nodeList = NodeManager.GetInstance().GetAllNodeGameObject();

            foreach (var node in nodeList)
            {
                float width01 = nodeGameObject.GetComponent<BoxCollider2D>().size.x;
                float height01 = nodeGameObject.GetComponent<BoxCollider2D>().size.y;
                
                if (nodeGameObject != node)
                {
                    float width02 = nodeGameObject.GetComponent<BoxCollider2D>().size.x;
                    float height02 = nodeGameObject.GetComponent<BoxCollider2D>().size.y;

                    var position01 = nodeGameObject.transform.position;
                    var position02 = node.transform.position;
                    float dx = Mathf.Abs(position01.x - position02.x);
                    float dy = Mathf.Abs(position01.y - position02.y);

                    if (2 * dx < width01 + width02 && 2 * dy < height01 + height02)
                    {
                        collisionNodeGameObjects.Add(node);
                    }
                }
            }

            return collisionNodeGameObjects;
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
            
            float collisionNode01HorizontalScalar = width + delta - dx1;
            float collisionNode01VerticalScalar = height + delta - dy1;
            float collisionNode02HorizontalScalar = width + delta - dx2;
            float collisionNode02VerticalScalar = height + delta - dy2;
            
            Vector3 newPosition = Vector3.zero;

            if (dx1Sign != dx2Sign && dy1Sign == dy2Sign)
            {
                if (collisionNode01VerticalScalar > collisionNode02VerticalScalar)
                {
                    newPosition =
                        targetNode.transform.position +
                        new Vector3(collisionNode01HorizontalScalar * (dx1Sign ? 1 : -1),
                            collisionNode02VerticalScalar * (dy2Sign ? 1 : -1), 0);
                }
                else
                {
                    newPosition =
                    targetNode.transform.position +
                        new Vector3(collisionNode02HorizontalScalar * (dx2Sign ? 1 : -1),
                            collisionNode01VerticalScalar * (dy1Sign ? 1 : -1), 0);
                }
            }

            if (dx1Sign == dx2Sign && dy1Sign != dy2Sign)
            {
                if (collisionNode01HorizontalScalar > collisionNode02HorizontalScalar)
                {
                    newPosition =
                    targetNode.transform.position +
                        new Vector3(collisionNode02HorizontalScalar * (dx2Sign ? 1 : -1),
                            collisionNode01VerticalScalar * (dy1Sign ? 1 : -1), 0);
                }
                else
                {
                    newPosition =
                    targetNode.transform.position +
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
                    newPosition =
                    targetNode.transform.position +
                        new Vector3(collisionNode02HorizontalScalar * (dx2Sign ? 1 : -1),
                            collisionNode01VerticalScalar * (dy1Sign ? 1 : -1), 0);
                }
                else
                {
                    newPosition =
                    targetNode.transform.position +
                        new Vector3(collisionNode01HorizontalScalar * (dx1Sign ? 1 : -1),
                            collisionNode02VerticalScalar * (dy2Sign ? 1 : -1), 0);
                }
                
                NodeManipulator.GetInstance()
                    .MoveNodePosition(NodeManager.GetInstance().GetNodeByGameObject(targetNode), newPosition);
            }
        }
        
        private void AdjustNodePosition(GameObject targetNode, GameObject collisionNode)
        {
            Vector3 newPosition = Vector3.zero;
            Vector3 delta = targetNode.transform.position - benchmark;

            if (width - Mathf.Abs(delta.x) < height - Mathf.Abs(delta.y))
            {
                if (delta.x > 0)
                {
                    newPosition.x = benchmark.x + width + this.delta;
                }
                else
                {
                    newPosition.x = benchmark.x - width - this.delta;
                }
                newPosition.y = targetNode.transform.position.y;
            }
            else
            {
                if (delta.y> 0)
                {
                    newPosition.y = benchmark.y + height + this.delta;
                }
                else
                {
                    newPosition.y = benchmark.y - height - this.delta;
                }
                newPosition.x = targetNode.transform.position.x;
            }

            NodeManipulator.GetInstance()
                .MoveNodePosition(NodeManager.GetInstance().GetNodeByGameObject(targetNode), newPosition);
        }
        
    }
}

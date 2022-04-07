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

        private float delta = 0.2f;

        public void CheckCollision(GameObject nodeGameObject)
        {
            List<GameObject> collisionNodeList = GetCollisionNodeList(nodeGameObject);

            if (collisionNodeList.Count == 1)
            {
                AdjustNodePosition(nodeGameObject, collisionNodeList[0]);
            }

            if (collisionNodeList.Count == 2)
            {
                AdjustNodePosition(nodeGameObject, collisionNodeList[0], collisionNodeList[1]);
            }

            if (collisionNodeList.Count > 2)
            {
                SearchPosition(nodeGameObject);
            }
            
            if (GetCollisionNodeList(nodeGameObject).Count>0)
            {
                SearchPosition(nodeGameObject);
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
                    float width02 = node.GetComponent<BoxCollider2D>().size.x;
                    float height02 = node.GetComponent<BoxCollider2D>().size.y;

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

        private void AdjustNodePosition(GameObject targetNode, GameObject collisionNode01)
        {
            Vector3 targetNodePosition = targetNode.transform.position;

            float width = targetNode.GetComponent<BoxCollider2D>().size.x;
            float height = targetNode.GetComponent<BoxCollider2D>().size.y;

            float width01 = collisionNode01.GetComponent<BoxCollider2D>().size.x;
            float height01 = collisionNode01.GetComponent<BoxCollider2D>().size.y;

            var position = collisionNode01.transform.position;
            float dx = (width + width01) / 2f - Mathf.Abs(targetNodePosition.x - position.x);
            bool signX = position.x - targetNodePosition.x > 0;
            float dy = (height + height01) / 2f - Mathf.Abs(targetNodePosition.y - position.y);
            bool signY = position.y - targetNodePosition.y > 0;

            if (dx > dy)
            {
                targetNodePosition.y = signY ? targetNodePosition.y - dy - delta : targetNodePosition.y + dy + delta;
            }
            else
            {
                targetNodePosition.x = signX ? targetNodePosition.x - dx - delta : targetNodePosition.x + dx + delta;
            }

            NodeManipulator.GetInstance()
                .MoveNodePosition(NodeManager.GetInstance().GetNodeByGameObject(targetNode), targetNodePosition);
        }

        private void AdjustNodePosition(GameObject targetNode, GameObject collisionNode01, GameObject collisionNode02)
        {
            float width = targetNode.GetComponent<BoxCollider2D>().size.x;
            float height = targetNode.GetComponent<BoxCollider2D>().size.y;

            float width01 = collisionNode01.GetComponent<BoxCollider2D>().size.x;
            float height01 = collisionNode01.GetComponent<BoxCollider2D>().size.y;

            float width02 = collisionNode02.GetComponent<BoxCollider2D>().size.x;
            float height02 = collisionNode02.GetComponent<BoxCollider2D>().size.y;

            var targetNodePosition = targetNode.transform.position;
            var position01 = collisionNode01.transform.position;
            var position02 = collisionNode02.transform.position;
            float collision01W = (width + width01) / 2f - Mathf.Abs(targetNodePosition.x - position01.x);
            bool w01Sign = position01.x - targetNodePosition.x > 0;
            float collision01H = (height + height01) / 2f - Mathf.Abs(targetNodePosition.y - position01.y);
            bool h01Sign = position01.y - targetNodePosition.y > 0;
            float collision02W = (width + width02) / 2f - Mathf.Abs(targetNodePosition.x - position02.x);
            bool w02Sign = position02.x - targetNodePosition.x > 0;
            float collision02H = (height + height02) / 2f - Mathf.Abs(targetNodePosition.y - position02.y);
            bool h02Sign = position02.y - targetNodePosition.y > 0;

            if (Mathf.Abs(collision01W) + Math.Abs(collision02H) < Mathf.Abs(collision02W) + Math.Abs(collision01H))
            {
                targetNodePosition.x = w01Sign
                    ? targetNodePosition.x - collision01W - delta
                    : targetNodePosition.x + collision01W + delta;
                targetNodePosition.y = h02Sign
                    ? targetNodePosition.y - collision02H - delta
                    : targetNodePosition.y += collision02H + delta;
            }
            else
            {
                targetNodePosition.x = w02Sign
                    ? targetNodePosition.x - collision02W - delta
                    : targetNodePosition.x + collision02W + delta;
                targetNodePosition.y = h01Sign
                    ? targetNodePosition.y - collision01H - delta
                    : targetNodePosition.y += collision01H + delta;
            }

            NodeManipulator.GetInstance()
                .MoveNodePosition(NodeManager.GetInstance().GetNodeByGameObject(targetNode), targetNodePosition);
        }

        private void SearchPosition(GameObject targetNode)
        {
            Vector3 originalPosition = targetNode.transform.position;

            int count = 0;
            while (true)
            {
                if (count > 50)
                {
                    Debug.LogError("Can not find optimal Node Position");
                    return;
                }

                for (int x = -count; x < count + 1; x++)
                {
                    for (int y = -count; y < count + 1; y++)
                    {
                        if (x == -count || x == count || y == -count || y == count)
                        {
                            Vector3 newPosition = originalPosition;
                            newPosition.x +=  x / 10f;
                            newPosition.y +=  y / 10f;
                            targetNode.transform.position = newPosition;
                            List<GameObject> collisionList = GetCollisionNodeList(targetNode);
                            if (collisionList.Count == 0)
                            {
                                newPosition.x += Mathf.Sign(x) * delta;
                                newPosition.y += Mathf.Sign(y) * delta;
                                NodeManipulator.GetInstance()
                                    .MoveNodePosition(NodeManager.GetInstance().GetNodeByGameObject(targetNode),
                                        newPosition);
                                return;
                            }
                        }
                    }
                }

                count++;
            }
        }
    }
}

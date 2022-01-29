using System;
using System.Collections.Generic;
using UnityEngine;

namespace StoryBoardEditor
{
    public class StoryBoardEditorGridSystem : MonoBehaviour
    {
        #region Singleton

        private static StoryBoardEditorGridSystem _instance;

        public static StoryBoardEditorGridSystem GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<StoryBoardEditorGridSystem>();
                if (obj == null)
                {
                    Debug.LogError("StoryBoardEditorGridSystem is not ready");
                    return null;
                }

                _instance = obj;
            }
            return _instance;
        }

        #endregion

        [SerializeField] private GameObject horizontalThickLineLayer;
        [SerializeField] private GameObject verticalThickLineLayer;
        
        [SerializeField] private GameObject horizontalLineLayer;
        [SerializeField] private GameObject verticalLineLayer;
        [SerializeField] private GameObject thickLine;
        [SerializeField] private GameObject line;

        private readonly List<GameObject> _horizontalThickLineList = new List<GameObject>();
        private readonly List<GameObject> _verticalThickLineList = new List<GameObject>();
        private readonly List<GameObject> _horizontalLineList = new List<GameObject>();
        private readonly List<GameObject> _verticalLineList = new List<GameObject>();
        
        private const int Max =9;

        private bool FloatRemainderOperation(float dividend, float divisor)
        {
            for (int i = 0; i < 10; i++)
            {
                if (Math.Abs(dividend - divisor * i)<0.1f)
                {
                    return true;
                }
            }
            return false;
        }
        
        public void AdjustGridLine()
        {
            float cameraSize = Camera.main.orthographicSize;
            float size = cameraSize / 2f;
            
            if (FloatRemainderOperation(cameraSize,2f))
            {
                for (int i = 0; i < _horizontalThickLineList.Count; i++)
                {
                    int num = -1 * Max + i;
                    _horizontalThickLineList[i].transform.localPosition = new Vector3(0, num * size, 0);
                    _horizontalThickLineList[i].transform.localScale = new Vector3(10000, 2 * size, 1);
                    _verticalThickLineList[i].transform.localPosition = new Vector3(num * size, 0, 0);
                    _verticalThickLineList[i].transform.localScale = new Vector3(2 * size, 10000, 1);
                }

                for (int i = 0; i < _horizontalLineList.Count; i++)
                {
                    float num = -1 * Max + i / 5f;
                    ;
                    _horizontalLineList[i].transform.localPosition = new Vector3(0, num * size, 0);
                    _horizontalLineList[i].transform.localScale = new Vector3(10000, 1 * size, 1);
                    _verticalLineList[i].transform.localPosition = new Vector3(num * size, 0, 0);
                    _verticalLineList[i].transform.localScale = new Vector3(1 * size, 10000, 1);
                }
            }
        }

        public void MakeGridLine()
        {
            for (int i = 0; i < Max*2+1; i++)
            {
                int num = -1 * Max + i;
                GameObject newLineH = Instantiate(thickLine,horizontalThickLineLayer.transform);
                newLineH.name = "ThickLine_H" + num;
                newLineH.transform.localPosition = new Vector3(0, num, 0);
                newLineH.transform.localScale = new Vector3(10000, 2, 1);
                _horizontalThickLineList.Add(newLineH);
                
                GameObject newLineR = Instantiate(thickLine,verticalThickLineLayer.transform);
                newLineR.name = "ThickLine_R" + num;
                newLineR.transform.localPosition = new Vector3(num, 0, 0);
                newLineR.transform.localScale = new Vector3(2, 10000, 1);
                _verticalThickLineList.Add(newLineR);
            }

            for (int i = 0; i < Max*10+1; i++)
            {
                float num = -1 * Max + i / 5f;
                GameObject newLineH = Instantiate(line,horizontalLineLayer.transform);
                newLineH.name = "Line_H" + num;
                newLineH.transform.localPosition = new Vector3(0, num, 0);
                newLineH.transform.localScale = new Vector3(10000, 1, 1);
                _horizontalLineList.Add(newLineH);
                
                GameObject newLineR = Instantiate(thickLine,verticalLineLayer.transform);
                newLineR.name = "Line_R" + num;
                newLineR.transform.localPosition = new Vector3(num, 0, 0);
                newLineR.transform.localScale = new Vector3(1, 10000, 1);
                _verticalLineList.Add(newLineR);
            }
        }

        private void OnEnable()
        {
            MakeGridLine();
        }

        public void Update()
        {
            AdjustGridLine();
        }
    
        public Vector3 SetPositionToGrid(Vector3 vec3)
        {
            return new Vector3((float) Math.Round(vec3.x), (float) Math.Round(vec3.y), 0);
        }
    }
}

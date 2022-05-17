using System.Collections.Generic;
using UnityEngine;

namespace ResearchSystem
{
    public class ResearchObjectSetLoadManger : MonoBehaviour
    {
        #region Singleton

        private static ResearchObjectSetLoadManger _instance;

        public static ResearchObjectSetLoadManger GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<ResearchObjectSetLoadManger>();
                if (obj == null)
                {
                    Debug.LogError("Error! ResearchObjectSetLoadManger is disable now");
                    return null;
                }

                _instance = obj;
            }
            return _instance;
        }

        #endregion
    
        private readonly Dictionary<string,GameObject> _objectSetList = new Dictionary<string, GameObject>();
    
        public void LoadAllObjectSet()
        {
            GameObject[] list = Resources.LoadAll<GameObject>("ResearchObjectSet");
            foreach (var objectSet in list)
            {
                _objectSetList.Add(objectSet.name,objectSet);
            }
        }

        public void LoadObjectSet(string researchId)
        {
            if (this.transform.childCount > 0)
            {
                Destroy(this.transform.GetChild(0).gameObject);
            }
            GameObject objectSet = Instantiate(_objectSetList[researchId],this.transform);
            // 진행상황 적용 필요
        }

        public void DisableObjectSet()
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }

        public void EnableObjectSet()
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}

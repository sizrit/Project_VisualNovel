using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StoryBoardSystem
{
    public enum ImageId
    {
        Null,
        Image01,
    }
    
    public class StoryBoardImageLoadManager : MonoBehaviour
    {
        #region Singleton

        private static StoryBoardImageLoadManager _instance;

        public static StoryBoardImageLoadManager GetInstance()
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<StoryBoardImageLoadManager>();
                if (obj == null)
                {
                    Debug.LogError("Error! StoryBoardImageLoadManager is disable now");
                    return null;
                }
                _instance = obj;
            }

            return _instance;
        }

        #endregion

        private readonly Dictionary<ImageId, GameObject> _imagePrefabsList = new Dictionary<ImageId, GameObject>();

        public void LoadAllPrefabs()
        {
            GameObject[] imagePrefabs = Resources.LoadAll<GameObject>("Images/Prefabs");
            
            foreach (var imagePrefab in imagePrefabs)
            {
                _imagePrefabsList.Add(ConvertStringToImageId(imagePrefab.name), imagePrefab);
            }
        }

        public static ImageId ConvertStringToImageId(string stringValue)
        {
            List<ImageId> enumValueList = Enum.GetValues(typeof(ImageId)).Cast<ImageId>().ToList();
            foreach (var enumValue in enumValueList)
            {
                if (enumValue.ToString() == stringValue)
                {
                    return enumValue;
                }
            }

            return ImageId.Null;
        }

        public void SetImage(ImageId imageIdValue)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                Destroy(this.transform.GetChild(i).gameObject);
            }

            Instantiate(_imagePrefabsList[imageIdValue], this.transform);
        }
    }
}

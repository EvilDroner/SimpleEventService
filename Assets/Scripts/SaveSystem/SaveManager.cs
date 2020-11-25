using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem
{
    public class SaveManager
    {
        
        private static SaveManager _instance;

        // TODO:: Зарефакторить с DI контейнерами
        public static SaveManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SaveManager();
                }

                return _instance;
            }
        }
        
        public string SaveFolderPath
        {
            get => Application.persistentDataPath + "/SaveFiles";
        }

        public void Save<T>(ISaveable<T> saveable) where T: class
        {
            if (!System.IO.Directory.Exists(SaveFolderPath))
            {
                System.IO.Directory.CreateDirectory(SaveFolderPath);
            }

            try
            {
                T saveData = saveable.Save();
                string saveFilePath = GetSaveFilePath(saveable.SaveId);
                string fileContents = JsonConvert.SerializeObject(saveData);
                System.IO.File.WriteAllText(saveFilePath, fileContents);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("An exception occured while saving " + saveable.SaveId);
                Debug.LogException(ex);
            }
        }

        public T LoadSave<T>(string saveId) where T: class
        {
            string saveFilePath = GetSaveFilePath(saveId);

            if (System.IO.File.Exists(saveFilePath))
            {
                string fileContents = System.IO.File.ReadAllText(saveFilePath);

                return JsonConvert.DeserializeObject<T>(fileContents);
            }

            return null;
        }
        
        private string GetSaveFilePath(string saveId)
        {
            return string.Format($"{SaveFolderPath}/{saveId}.json");
        }
    }
}
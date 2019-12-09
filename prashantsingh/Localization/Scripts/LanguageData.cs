using System.Collections.Generic;
using UnityEngine;
namespace Prashant.Localization
{
    [System.Serializable]
    public class Translation
    {
        public string id;
        public string translatedWord;

        public Translation(string tempWord = "", string tempTranslation = "")
        {
            id = tempWord;
            translatedWord = tempTranslation;
        }

    }

    [CreateAssetMenu(fileName = "LanguageData", menuName = "Localization/Create Language")]
    public class LanguageData : ScriptableObject
    {
        public List<Translation> wordsCollection = new List<Translation>();

        public void UpdateWordsList(List<Translation> tempColl)
        {
            for (int count = 0; count < tempColl.Count; count++)
            {
                if (wordsCollection.Find(x => (x.id.Equals(tempColl[count].id))) == null)
                {
                    wordsCollection.Add(new Translation(tempColl[count].id));
                }
            }
        }

        public void MoveUp(int currIndex)
        {
            Translation tempTranslation1 = wordsCollection[currIndex];
            Translation tempTranslation2 = wordsCollection[currIndex - 1];
            wordsCollection[currIndex] = tempTranslation2;
            wordsCollection[currIndex - 1] = tempTranslation1;

        }

        public void MoveDown(int currIndex)
        {
            Translation tempTranslation1 = wordsCollection[currIndex];
            Translation tempTranslation2 = wordsCollection[currIndex + 1];
            wordsCollection[currIndex] = tempTranslation2;
            wordsCollection[currIndex + 1] = tempTranslation1;

        }

    }
}
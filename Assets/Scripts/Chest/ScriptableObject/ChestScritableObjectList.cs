using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    [CreateAssetMenu(fileName = "ListOfChests", menuName = "ScriptableObjects/ChestsList")]
    public class ChestScritableObjectList : ScriptableObject
    {
        public List<ChestScriptableObject> chestScriptableList;
    }
}

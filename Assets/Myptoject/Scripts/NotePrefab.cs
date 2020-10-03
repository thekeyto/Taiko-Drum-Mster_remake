using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New NoteList", menuName = "Inventory/New NoteList")]
public class NotePrefab : ScriptableObject
{
    public List<GameObject> itemlist = new List<GameObject>();
}
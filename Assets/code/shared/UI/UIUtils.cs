using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UIUtils
{
    public enum Masks
    {
        Cards,
        Stock,

        Foundation0,
        Foundation1,
        Foundation2,
        Foundation3,

        Tableu0,
        Tableu1,
        Tableu2,
        Tableu3,
        Tableu4,
        Tableu5,
        Tableu6,
    }
    private static Dictionary<Masks, string> MaskMappings = new Dictionary<Masks, string> {
        { Masks.Cards, "Cards" },
        { Masks.Stock, "Stock" },

        { Masks.Foundation0, "Foundation0" },
        { Masks.Foundation1, "Foundation1" },
        { Masks.Foundation2, "Foundation2" },
        { Masks.Foundation3, "Foundation3" },

        { Masks.Tableu0, "Tableu0" },
        { Masks.Tableu1, "Tableu1" },
        { Masks.Tableu2, "Tableu2" },
        { Masks.Tableu3, "Tableu3" },
        { Masks.Tableu4, "Tableu4" },
        { Masks.Tableu5, "Tableu5" },
        { Masks.Tableu6, "Tableu6" }
    };

    public static GameObject GetGameObject(Vector3 position, Masks mask)
    {
        if (!MaskMappings.ContainsKey(mask))
            return null;

        MaskMappings.TryGetValue(mask, out var maskName);

        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = position;
        var allHits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, allHits);

        for(int i = allHits.Count - 1; i >= 0; i--)
        {
            if(allHits[i].gameObject.layer != LayerMask.NameToLayer(maskName))
                allHits.RemoveAt(i);
        }

        return allHits.Count > 0 ? allHits[0].gameObject : null;
    }

}

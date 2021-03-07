using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class AttributesGenerator
{

    public List<Attribute> GenerateAttributes(AttributesGenerationProfile attributeProfile)
    {
        if (attributeProfile == null)
        {
            return new List<Attribute>();
        }

        if (attributeProfile.MinNumberOfAttributes < 0 ||
            attributeProfile.MaxNumberOfAttributes > attributeProfile.PossibleAttributes.Count)
        {
            Debug.LogError($"The minimum and maximum number of attributes is out of range of the possible attributes.");
            return new List<Attribute>();
        }

        var numberOfAttributes = Random.Range(attributeProfile.MinNumberOfAttributes, attributeProfile.MaxNumberOfAttributes + 1);
        var attributes = attributeProfile.PossibleAttributes.OrderBy(x => Random.Range(0, 100)).Take(numberOfAttributes);

        if (attributeProfile.GuaranteedAttributes != null &&
            attributeProfile.GuaranteedAttributes.Count > 0)
        {
            attributes = attributes.Concat(attributeProfile.GuaranteedAttributes);
        }

        return attributes.ToList();
    }
}

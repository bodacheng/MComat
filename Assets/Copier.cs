using UnityEngine;

public static class Copier<TParent, TChild> where TParent : class
    where TChild : class
{
    public static void Copy(TParent parent, TChild child)
    {
        var parentFields = parent.GetType().GetFields();
        var childFields = child.GetType().GetFields();

        foreach (var parentField in parentFields)
        {
            foreach (var childField in childFields)
            {
                if (parentField.Name == childField.Name && parentField.FieldType == childField.FieldType)
                {
                    //Debug.Log(parentField.Name +" : "+ parentField.GetValue(parent));
                    childField.SetValue(child, parentField.GetValue(parent));
                    break;
                }
            }
        }
    }
}
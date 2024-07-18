using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public static class AssetRenamer
{
    private static readonly Type[] NotAllowedTypes = new[]
    {
        typeof(MonoScript),
    };

    [MenuItem("Assets/Rename Assets")]
    private static void RenameAsset()
    {
        //Get the selected assets
        Object[] selectedAssets = Selection.GetFiltered(typeof(Object), SelectionMode.Assets);

        bool renamedAtLeastOneAsset = false;

        foreach (Object selectedAsset in selectedAssets)
        {
            //Check if the asset is in the not allowed types
            Type assetType = selectedAsset.GetType();
            if (NotAllowedTypes.Contains(assetType))
            {
                Debug.LogWarning($"[{nameof(AssetRenamer)}] Asset Type: {assetType} is not allowed to be renamed");
                continue;
            }

            //Rename the asset
            string oldName = selectedAsset.name;
            string newName = GetFixedName(selectedAsset.name);
            if (oldName == newName) continue;

            //Get the asset path
            string assetPath = AssetDatabase.GetAssetPath(selectedAsset);
            string errorMessage = AssetDatabase.RenameAsset(assetPath, newName);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                Debug.LogError($"[{nameof(AssetRenamer)}] Error renaming asset: {errorMessage}");
                continue;
            }

            renamedAtLeastOneAsset = true;
            Debug.Log($"[{nameof(AssetRenamer)}] Renamed Asset: {oldName} to {newName}");
        }

        if (!renamedAtLeastOneAsset)
        {
            Debug.Log($"[{nameof(AssetRenamer)}] No assets were renamed");
            return;
        }

        AssetDatabase.Refresh();
    }

    private static string GetFixedName(string oldName)
    {
        //Here you can Write you Rename Rules and logic

        //trim
        oldName = oldName.Trim();

        //Replace underscores with spaces
        oldName = oldName.Replace("_", " ");

        //Replace dashes with spaces
        oldName = oldName.Replace("-", " ");

        //Remove duplicate spaces
        oldName = System.Text.RegularExpressions.Regex.Replace(oldName, @"\s+", " ");

        //Separate camel case
        oldName = System.Text.RegularExpressions.Regex.Replace(oldName, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ");

        //To lower
        oldName = oldName.ToLower();

        //Separate numbers from letters
        oldName = System.Text.RegularExpressions.Regex.Replace(oldName, "([a-zA-Z])([0-9])", "$1 $2");

        //Capitalize the first letter of each word
        oldName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(oldName);

        return oldName;
    }
}
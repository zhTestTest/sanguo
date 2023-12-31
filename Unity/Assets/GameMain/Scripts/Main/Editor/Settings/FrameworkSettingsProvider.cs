using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UIElements;

public class FrameworkSettingsProvider : SettingsProvider
{
    const string k_FrameworkSettingsPath = "Assets/GameMain/Resources/Settings/FrameworkSettings.asset";
    private const string headerName = "GameMain/FrameworkSettings";
    private SerializedObject m_CustomSettings;

    internal static SerializedObject GetSerializedSettings()
    {
        return new SerializedObject(FrameworkSettingsUtils.FrameworkSettings);
    }
    public static bool IsSettingsAvailable()
    {
        return File.Exists(k_FrameworkSettingsPath);
    }

    public override void OnActivate(string searchContext, VisualElement rootElement)
    {
        base.OnActivate(searchContext, rootElement);
        m_CustomSettings = GetSerializedSettings();
    }

    public override void OnGUI(string searchContext)
    {
        base.OnGUI(searchContext);
        using var changeCheckScope = new EditorGUI.ChangeCheckScope();
        EditorGUI.BeginDisabledGroup(true);
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("m_ScriptAuthor"));
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("m_ScriptVersion"));
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("m_AppStage"));
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("m_DefaultFont"));
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("BaseAssetsRootName"));
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("m_ResourcesArea"));
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("m_ResourceVersionFileName"));
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("WindowsAppUrl"));
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("MacOSAppUrl"));
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("IOSAppUrl"));
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("AndroidAppUrl"));
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("m_CurUseServerChannel"));
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("m_ServerChannelInfos"));
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("m_IsReadLocalConfigInEditor"));
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("m_ConfigVersionFileName"));
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("m_ConfigFolderName"));
        EditorGUILayout.Space(20);
        if ( !changeCheckScope.changed ) return;
        m_CustomSettings.ApplyModifiedPropertiesWithoutUndo();
    }

    public FrameworkSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(path, scopes, keywords)
    {
    }
    [SettingsProvider]
    private static SettingsProvider CreateSettingProvider()
    {
        if (IsSettingsAvailable())
        {
            var provider = new FrameworkSettingsProvider(headerName, SettingsScope.Project);
            provider.keywords = GetSearchKeywordsFromGUIContentProperties<FrameworkSettings>();
            return provider;
        }
        return null;
    }
}

using Fantasy.Helper;
using Fantasy.Hotfix.Generate.ConfigTable;
using Fantasy;

public static class SceneHelper
{
    public static long GetSceneEntityId(uint routeId)
    {
        var enityId = 0L;
        
        foreach (var sceneConfig in SceneConfigData.Instance.List)
        {
            if (sceneConfig.ServerConfigId == routeId)
            {
                enityId = sceneConfig.EntityId;
                break;
            }
        }
        return enityId;
    }
}
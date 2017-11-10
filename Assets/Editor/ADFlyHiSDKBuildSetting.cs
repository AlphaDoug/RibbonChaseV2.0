using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.Xml;
#endif
using System.IO;

public static class ADFlyHiSDKBuildSetting
{

    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
    {
        if (buildTarget != BuildTarget.iOS)
        {
            Debug.Log("这不是IOS平台，不执行操作");
            return;
        }
        
        string projPath = PBXProject.GetPBXProjectPath(path);
        PBXProject proj = new PBXProject();

        proj.ReadFromString(File.ReadAllText(projPath));
        string target = proj.TargetGuidByName("Unity-iPhone");

        //广告依赖库
        List<string> frameworks = new List<string>();
        frameworks.Add("AppcoachsSDK.framework");
        frameworks.Add("GoogleMobileAds.framework");
        frameworks.Add("InMobiSDK.framework");
        frameworks.Add("MVSDK.framework");
        frameworks.Add("MVSDKAppWall.framework");
        frameworks.Add("MVSDKInterstitial.framework");
        frameworks.Add("MVSDKOfferWall.framework");
        frameworks.Add("MVSDKReward.framework");
        frameworks.Add("UnityAds.framework");
        frameworks.Add("VungleSDK.framework");

        //添加依赖库到工程中
        foreach (string framework in frameworks)
        {
            CopyAndReplaceDirectory("Assets/ADFlyHiSDK/Plugins/iOS/frameworks/" + framework, Path.Combine(path, "ADFlyHiSDK/Plugins/iOS/frameworks/" + framework));

            string fileGuid = proj.FindFileGuidByProjectPath("Frameworks/ADFlyHiSDK/Plugins/iOS/frameworks/" + framework);
            proj.RemoveFileFromBuild(target, fileGuid);
            proj.RemoveFile(fileGuid);

            string name = proj.AddFile("ADFlyHiSDK/Plugins/iOS/frameworks/" + framework, "ADFlyHiSDK/Plugins/iOS/frameworks/" + framework, PBXSourceTree.Source);
            proj.AddFileToBuild(target, name);
        }

        DirectoryInfo di = new DirectoryInfo(Path.Combine(path, "Frameworks/ADFlyHiSDK/"));
        di.Delete(true);

        List<string> bundles = new List<string>();
        bundles.Add("appcoachsSDK.bundle");
        bundles.Add("ChanceAdRes.bundle");

        foreach (string bundle in bundles)
        {
            CopyAndReplaceDirectory("Assets/ADFlyHiSDK/Plugins/iOS/resources/" + bundle, Path.Combine(path, "ADFlyHiSDK/Plugins/iOS/frameworks/" + bundle));
            string name = proj.AddFile("ADFlyHiSDK/Plugins/iOS/frameworks/" + bundle, "ADFlyHiSDK/Plugins/iOS/frameworks/" + bundle, PBXSourceTree.Source);
            proj.AddFileToBuild(target, name);
        }

        //tbd依赖包
        List<string> tbds = new List<string>();
        tbds.Add("libsqlite3.tbd");
        tbds.Add("libz.tbd");
        tbds.Add("libz.1.2.5.tbd");
        tbds.Add("libc++.tbd");
        tbds.Add("libxml2.tbd");

        //添加tbd依赖包
        foreach(string tbd in tbds)
        {
            string name = proj.AddFile("usr/lib/" + tbd, "Frameworks/" + tbd, PBXSourceTree.Sdk);
            proj.AddFileToBuild(target, name);
        }

        Debug.Log("添加其他资源");
        File.Copy("Assets/ADFlyHiSDK/Plugins/iOS/resources/mz_noVoice@2x.png", Path.Combine(path, "ADFlyHiSDK/Plugins/iOS/frameworks/mz_noVoice@2x.png"));
        proj.AddFile("ADFlyHiSDK/Plugins/iOS/frameworks/mz_noVoice@2x.png", "ADFlyHiSDK/Plugins/iOS/frameworks/mz_noVoice@2x.png", PBXSourceTree.Source);

        File.Copy("Assets/ADFlyHiSDK/Plugins/iOS/resources/mz_voice_normal@2x.png", Path.Combine(path, "ADFlyHiSDK/Plugins/iOS/frameworks/mz_voice_normal@2x.png"));
        proj.AddFile("ADFlyHiSDK/Plugins/iOS/frameworks/mz_voice_normal@2x.png", "ADFlyHiSDK/Plugins/iOS/frameworks/mz_voice_normal@2x.png", PBXSourceTree.Source);

        //设置buildsetting
        Debug.Log("设置buildsetting");
        proj.SetBuildProperty(target, "OTHER_LDFLAGS", "-ObjC");
        proj.SetBuildProperty(target, "ENABLE_BITCODE", "NO");
        proj.SetBuildProperty(target, "FRAMEWORK_SEARCH_PATHS", "$(inherited)");
        proj.AddBuildProperty(target, "FRAMEWORK_SEARCH_PATHS", "$(PROJECT_DIR)/ADFlyHiSDK/Plugins/iOS/frameworks/");

        proj.WriteToFile(projPath);

        //修改Info.plist
        Debug.Log("修改info.plist");
        string plistPath = path + "/Info.plist";
        PlistDocument plist = new PlistDocument();

        plist.ReadFromString(File.ReadAllText(plistPath));
        PlistElementDict rootDic = plist.root.AsDict();

        rootDic.CreateDict("NSAppTransportSecurity").SetBoolean("NSAllowsArbitraryLoads", true);
        rootDic.SetBoolean("UIStatusBarHidden", true);
        rootDic.SetBoolean("UIViewControllerBasedStatusBarAppearance", false);
        rootDic.SetString("NSLocationWhenInUseUsageDescription", "");

        plist.WriteToFile(plistPath);
    }

    internal static void CopyAndReplaceDirectory(string srcPath, string dstPath)
    {
        if (dstPath.EndsWith(".meta"))
            return;

        if (dstPath.EndsWith(".DS_Store"))
            return;

        if (Directory.Exists(dstPath))
            Directory.Delete(dstPath);
        if (File.Exists(dstPath))
            File.Delete(dstPath);

        Directory.CreateDirectory(dstPath);

        foreach (var file in Directory.GetFiles(srcPath))
            File.Copy(file, Path.Combine(dstPath, Path.GetFileName(file)));

        foreach (var dir in Directory.GetDirectories(srcPath))
            CopyAndReplaceDirectory(dir, Path.Combine(dstPath, Path.GetFileName(dir)));
    }
}

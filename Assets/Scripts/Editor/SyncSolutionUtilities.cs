#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Editor
 {
     static class SyncSolutionUtilities
     {
         private static Type _syncVSType;
         private static MethodInfo _syncSolutionMethodInfo;
 
         private static FieldInfo _synchronizerField;
         private static object _synchronizerObject;
         private static Type _synchronizerType;
         private static MethodInfo _synchronizerSyncMethodInfo;
 
         static void GenerateSLN()
         {
             EditorApplication.ExecuteMenuItem("Assets/Sync C# Solution");
         }
 
 
         [MenuItem("Assets/Sync C# Solution", priority = 1000000)]
         public static void Sync()
         {
             Sync(false);
         }
 
         static void Sync(bool logsEnabled)
         {
             CleanOldFiles(logsEnabled);
             Call_SyncSolution(logsEnabled);
 
             // Quit if running in batchmode, should be handled by "-quit", but isn't
             if (Application.isBatchMode)
             {
                 EditorApplication.Exit(0);
             }
         }
 
         static void CleanOldFiles(bool logsEnabled)
         {
             var assetsDirectoryInfo = new DirectoryInfo(Application.dataPath);
             var projectDirectoryInfo = assetsDirectoryInfo.Parent;
 
             var files = GetFilesByExtensions(projectDirectoryInfo, "*.sln", "*.csproj");
             
             foreach (var file in files)
             {
                 if (logsEnabled)
                 {
                     Debug.Log($"Remove old solution file: {file.Name}");
                 }
                 file.Delete();
             }
         }
 
         static void Call_SyncSolution(bool logsEnabled)
         {
             if (logsEnabled)
             {
                 Debug.Log($"Coll method: SyncVS.Sync()");
             }
 
             AssetDatabase.Refresh();
 
             // TODO: Rider and possibly other code editors, use reflection to call this method.
             // To avoid conflicts and null reference exception, this is left as a dummy method.
             Unity.CodeEditor.CodeEditor.Editor.CurrentCodeEditor.SyncAll();
 
         }
 
 
         private static IEnumerable<FileInfo> GetFilesByExtensions(DirectoryInfo dir, params string[] extensions)
         {
             extensions = extensions ?? new[] { "*" };
             var files = Enumerable.Empty<FileInfo>();
             
             foreach (var ext in extensions)
             {
                 files = files.Concat(dir.GetFiles(ext));
             }
             
             return files;
         }
     }
 }
 #endif
using System.IO;
using Code.Services.SaveLoad;
using UnityEditor;

namespace Code.Editor
{
  public static class Tools
  {
    [MenuItem("Tools/ClearProgress")]
    public static void ClearProgress()
    {
      File.Delete(ProgressPath.ProgressFilePath);
    }
  }
}
using UnityEngine;

namespace Code.Services.SaveLoad
{
  public static class ProgressPath
  {
    private const string ProgressFileName = "Progress";
    public static readonly string ProgressFilePath = $"{Application.persistentDataPath}/{ProgressFileName}.dat";
  }
}
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Code.Data;
using Code.Services.PersistentProgress;
using Code.Services.ProgressWatchers;

namespace Code.Services.SaveLoad
{
  public class SaveLoadService : ISaveLoadService
  {
    private readonly ProgressWatchersService _progressWatchers;
    private readonly PersistentProgressService _progressService;

    public SaveLoadService(ProgressWatchersService progressWatchers, PersistentProgressService progressService)
    {
      _progressWatchers = progressWatchers;
      _progressService = progressService;
    }

    public void SaveProgress()
    {
      foreach (IProgressWriter progressWriter in _progressWatchers.Writers) 
        progressWriter.UpdateProgress(_progressService.Progress);
      
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      FileStream fileStream = File.Create(ProgressPath.ProgressFilePath);
      binaryFormatter.Serialize(fileStream, _progressService.Progress.ToJson());
      fileStream.Close();
    }

    public PlayerProgress LoadProgress()
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      if (!File.Exists(ProgressPath.ProgressFilePath))
        return null;
      FileStream fileStream = File.Open(ProgressPath.ProgressFilePath, FileMode.Open);
      PlayerProgress playerProgress = ((string)binaryFormatter.Deserialize(fileStream)).FromJson<PlayerProgress>();
      fileStream.Close();

      return playerProgress;
    }

  }
}
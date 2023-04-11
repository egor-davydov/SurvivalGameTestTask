using Code.Data;

namespace Code.Services.ProgressWatchers
{
  public interface IProgressReader
  {
    void ReceiveProgress(PlayerProgress progress);
  }
}
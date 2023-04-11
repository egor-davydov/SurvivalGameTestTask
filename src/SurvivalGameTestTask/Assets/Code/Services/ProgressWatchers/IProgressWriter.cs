using Code.Data;

namespace Code.Services.ProgressWatchers
{
  public interface IProgressWriter
  {
    void UpdateProgress(PlayerProgress progress);
  }
}
using Code.Data;

namespace Code.Services.SaveLoad
{
  public interface ISaveLoadService : IService
  {
    void SaveProgress();
    PlayerProgress LoadProgress();
  }
}
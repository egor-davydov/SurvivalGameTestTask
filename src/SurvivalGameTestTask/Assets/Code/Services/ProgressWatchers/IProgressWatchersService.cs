using System.Collections.Generic;
using UnityEngine;

namespace Code.Services.ProgressWatchers
{
  public interface IProgressWatchersService : IService
  {
    List<IProgressReader> Readers { get; }
    List<IProgressWriter> Writers { get; }
    void Register(GameObject gameObject);
  }
}
using System.Collections.Generic;
using UnityEngine;

namespace Code.Services.ProgressWatchers
{
  public class ProgressWatchersService : IProgressWatchersService
  {
    public List<IProgressReader> Readers { get; } = new();
    public List<IProgressWriter> Writers { get; } = new();

    public void Register(GameObject gameObject)
    {
      foreach (IProgressReader progressReader in gameObject.GetComponentsInChildren<IProgressReader>()) 
        Readers.Add(progressReader);
      foreach (IProgressWriter progressWriter in gameObject.GetComponentsInChildren<IProgressWriter>()) 
        Writers.Add(progressWriter);
    }
  }
}
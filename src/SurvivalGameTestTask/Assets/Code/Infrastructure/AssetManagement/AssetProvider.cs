using UnityEngine;

namespace Code.Infrastructure.AssetManagement
{
  public class AssetProvider : IAssetProvider
  {
    public GameObject Load(string assetPath) => 
      Resources.Load<GameObject>(assetPath);
  }
}
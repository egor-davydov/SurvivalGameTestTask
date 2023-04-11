using Code.Services;
using UnityEngine;

namespace Code.Infrastructure.AssetManagement
{
  public interface IAssetProvider : IService
  {
    GameObject Load(string assetPath);
  }
}
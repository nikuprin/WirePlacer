using System;
namespace WirePlacer.Services;

public interface IInputDataReader
{
    Task<List<double>> PickAndRead();
}

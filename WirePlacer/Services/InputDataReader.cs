using System.Globalization;

namespace WirePlacer.Services;

/// <summary>
///     Service for parsing input data.
/// </summary>
public class InputDataReader : IInputDataReader
{
    // for dot decimal separator
    private readonly IFormatProvider provider = new CultureInfo("en-US");
    private readonly NumberStyles style = NumberStyles.Number;

    public async Task<List<double>> PickAndRead()
    {
        var fileResult = await FilePicker.PickAsync();
        if (fileResult == null)
        {
            return new List<double>();
        }

        var radii = new List<double>();
        await using var stream = await fileResult.OpenReadAsync();
        using var streamReader = new StreamReader(stream);
        while (await streamReader.ReadLineAsync() is string line)
        {
            // ignore comments
            if (line.StartsWith("#"))
            {
                continue;
            }

            if (!double.TryParse(line, style, provider, out var radius))
            {
                continue;
            }

            radii.Add(radius);
        }

        return radii;
    }
}

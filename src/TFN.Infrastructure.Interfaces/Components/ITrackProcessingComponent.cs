using System.Collections.Generic;
using System.Threading.Tasks;

namespace TFN.Infrastructure.Interfaces.Components
{
    public interface ITrackProcessingComponent
    {
        Task TranscodeAudio(string sourceFilePath, string destinationFilePath);
        Task<List<int>> GetWaveform(string sourceFilePath, string destinationFilePath);
    }
}
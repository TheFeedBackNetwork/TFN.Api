using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TFN.Domain.Interfaces.Services
{
    public interface ITrackProcessingService
    {
        Task TranscodeAudio(string sourceFilePath, string destinationFilePath);
        Task<List<int>> GetWaveform(string sourceFilePath, string destinationFilePath);
    }
}

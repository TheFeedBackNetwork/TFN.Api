using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CSCore;

namespace TFN.Infrastructure.Interfaces.Components
{
    public interface ITrackProcessingComponent
    {
        Task TranscodeAudio(string sourceFilePath, string destinationFilePath);
        Task<List<int>> GetWaveform(string sourceFilePath, string destinationFilePath);


        Task<IWaveSource> GetWaveSourceAsync(Uri trackUri);
        Task<IWaveSource> GetWaveSourceAsync(Stream trackStream);
        Task<IReadOnlyList<int>> GetSoundWaveAsync(IWaveSource track);
        Task<Stream> TranscodeAudioAsync(IWaveSource track);
        Task<string> TranscodeAudioAsync(IWaveSource track, string fileName);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;
using Xunit;

namespace TFN.UnitTests.Domain.Model
{
    public class TrackTests
    {
        const string Category = "Track";

        private static Uri LocationDefault = new Uri("http://lol.com/lol");
        private static Guid TrackIdDefault = new Guid("ff169f0f-b9e6-446d-a0e8-54db590d3836");
        private static Guid UserIdDefault = new Guid("2a2c9a98-1853-4405-b41e-ca589a7c243e");
        private static string TrackNameDefault = "foo";
        public static TrackMetaData TrackMetaDataDefault = TrackMetaData.From(1,1,1,1,1,1,1);
        private static IReadOnlyList<int> SoundWaveDefault = Enumerable.Range(1,4000).ToList();
        private static DateTime CreatedDefault = new DateTime(2016, 4, 4, 5, 4, 4);

        public Track make_Track(Guid id, Guid userId, Uri location, string trackName, IReadOnlyList<int> soundWave, TrackMetaData metaData,
            DateTime created)
        {
            return Track.Hydrate(id, userId, location, soundWave, metaData, created);
        }

        public Track make_Track(IReadOnlyList<int> waveForm)
        {
            return make_Track(TrackIdDefault,UserIdDefault,LocationDefault, TrackNameDefault,waveForm,TrackMetaDataDefault,CreatedDefault);
        }

        public Track make_Track(TrackMetaData metaData)
        {
            return make_Track(TrackIdDefault,UserIdDefault,LocationDefault,TrackNameDefault,SoundWaveDefault,metaData,CreatedDefault);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(1)]
        [InlineData(3999)]
        [Trait("Category", Category)]
        public void Constructor_InvalidSoundWave_ArgumentExceptionThrown(int waveSize)
        {
            var waveForm = Enumerable.Range(1, waveSize).ToList();

            this.Invoking(x => x.make_Track(waveForm))
                .ShouldThrow<ArgumentException>();
        }

        [Fact]
        [Trait("Category", Category)]
        public void Constructor_InvalidTrackMetadata_ArgumentNullExceptionThrown()
        {
            TrackMetaData metadata = null;

            this.Invoking(x => x.make_Track(metadata))
                .ShouldThrow<ArgumentNullException>();
        }

    }
}
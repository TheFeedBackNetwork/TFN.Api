using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TFN.Domain.Architecture.Attributes;
using TFN.Domain.Architecture.Models;
using TFN.Domain.Models.Extensions;
using TFN.Domain.Models.Enums;

namespace TFN.Domain.Models.Entities
{
    [CacheVersion(0)]
    public class Post : MessageDomainEntity ,IAggregateRoot
    {
        public string TrackUrl { get; private set; }
        public string TrackName { get; private set; }
        public IReadOnlyList<string> Tags { get; private set; }
        public Genre Genre { get; private set; }

        [JsonConstructor]
        private Post(Guid id, Guid userId, string trackurl, string trackName, string text, Genre genre, IReadOnlyList<string> tags, bool isActive, DateTime created, DateTime modified)
            : base(id,userId,text,isActive,created,modified)
        {
            if(!trackurl.IsUrl())
            {
                throw new ArgumentException($"The url given [{nameof(trackurl)}] is not a valid track Url.");
            }

            TrackUrl = trackurl;
            TrackName = trackName;
            Genre = genre;
            Tags = tags;
        }

        public Post(Guid userId, string trackurl, string trackName, string text, Genre genre, IReadOnlyList<string> tags)
            :this(Guid.NewGuid(), userId,trackurl, trackName,text,genre,tags, true, DateTime.UtcNow,DateTime.UtcNow)
        {
            
        }

        public static Post Hydrate(Guid id, Guid userId, string trackurl, string trackName, string text, Genre genre, IReadOnlyList<string> tags, bool isActive, DateTime created, DateTime modified)
        {
            return new Post(id,userId,trackurl, trackName,text,genre,tags,isActive,created,modified);
        }

        public static Post EditPost(Post post, string text, string trackUrl, string trackName, IReadOnlyList<string> tags, Genre genre)
        {
            return new Post(post.Id, post.UserId, trackUrl, trackName, text, genre, tags, post.IsActive, post.Created, post.Modified);
        }
    }
}

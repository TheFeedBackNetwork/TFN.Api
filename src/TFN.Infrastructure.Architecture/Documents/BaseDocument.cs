﻿using System;
using Newtonsoft.Json;

namespace TFN.Infrastructure.Architecture.Documents
{
    public class BaseDocument : BaseDocument<Guid>
    {
        
    }
    public class BaseDocument<TKey> : IDocument<TKey>
    {

        [JsonProperty(PropertyName = "id", Order = -2)]
        public TKey Id { get; set; }

        [JsonProperty(PropertyName = "type", Order = -1)]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "created", Order = 1)]
        public DateTime Created { get; set; }

        [JsonProperty(PropertyName = "modified", Order = 2)]
        public DateTime Modified { get; set; }
        protected BaseDocument()
        {
            
        }

        public BaseDocument(TKey id, string type, DateTime created, DateTime modified)
            : this()
        {
            Id = id;
            Type = type;
            Created = created;
            Modified = modified;
        }
    }
}
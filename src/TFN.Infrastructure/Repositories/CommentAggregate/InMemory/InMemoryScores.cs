﻿using System.Collections.Generic;
using TFN.Domain.Models.Entities;

namespace TFN.Infrastructure.Repositories.CommentAggregate.InMemory
{
    public static class InMemoryScores
    {
        public static List<Score> Scores = new List<Score>();
    }
}
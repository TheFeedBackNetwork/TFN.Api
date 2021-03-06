﻿using System;
using System.IO;
using System.Threading.Tasks;

namespace TFN.Infrastructure.Interfaces.Components
{
    public interface IStorageComponent
    {
        Task<Uri> Upload(Stream trackStream, string container, string fileName);
        Task Delete(string container, string fileName);
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Shared.Service
{
    public interface IService<T> where T : class
    {
        Task<List<T>> GetAll();

        Task Add(T x);
    }
}

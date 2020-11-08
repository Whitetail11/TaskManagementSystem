﻿using DataLayer.Entities;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Interfaces
{
    public interface IFileRepository: IRepository<File>
    {
        File GetFile(int id);
    }
}
using BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IStatusService
    {
        IEnumerable<StatusDTO> GetAll();
        string GetName(int id);
    }
}

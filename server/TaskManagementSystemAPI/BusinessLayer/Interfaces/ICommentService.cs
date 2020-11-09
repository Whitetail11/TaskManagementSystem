using BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ICommentService
    {
        IEnumerable<ShowCommentDTO> GetForTask(int taskId);
        IEnumerable<ShowCommentDTO> GroupComments(IEnumerable<ShowCommentDTO> comments);
        void Create(CreateCommentDTO createCommentDTO, string userId);
        void Delete(int id, int taskId);
        bool ExistAny(int id, string userId);
    }
}

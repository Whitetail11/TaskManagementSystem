using BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ICommentService
    {
        void Create(CreateCommentDTO createCommentDTO, string userId);
        void Delete(int id);
        bool ExistAny(int id, string userId);
        IEnumerable<ShowCommentDTO> GroupComments(IEnumerable<ShowCommentDTO> comments);
    }
}

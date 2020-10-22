using BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ICommentService
    {
        ShowCommentDTO Create(CreateCommentDTO createCommentDTO, string userId);
    }
}

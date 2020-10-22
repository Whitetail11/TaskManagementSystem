using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataLayer.Entities;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CommentService: ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ShowCommentDTO Create(CreateCommentDTO createCommentDTO, string userId)
        {
            var comment = _mapper.Map<CreateCommentDTO, Comment>(createCommentDTO);
            comment.Date = DateTime.Now;
            comment.UserId = userId;
            _unitOfWork.CommentRepository.Create(comment);

            return _mapper.Map<Comment, ShowCommentDTO>(comment);
        }
    }
}

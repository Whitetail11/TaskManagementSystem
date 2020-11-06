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
    public class StatusService: IStatusService
    {
        private readonly IStatusRepository _statusReopository;
        private readonly IMapper _mapper;

        public StatusService(IStatusRepository statusRepository, IMapper mapper)
        {
            _statusReopository = statusRepository;
            _mapper = mapper;
        }

        public IEnumerable<StatusDTO> GetAll()
        {
            var statuses = _statusReopository.GetAll();
            return _mapper.Map<IEnumerable<Status>, IEnumerable<StatusDTO>>(statuses);
        }
    }
}

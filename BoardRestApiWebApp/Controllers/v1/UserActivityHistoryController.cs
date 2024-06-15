using Datas.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;
using Entities;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestApiProject.Models;
using System.Threading;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Common.Utilities;
using Microsoft.AspNetCore.Mvc.Versioning;
namespace RestApiProject.Controllers.v1
{
    [ApiVersion("1")]
    public class UserActivityHistoryController : BaseController
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;
        public UserActivityHistoryController(IRepository<User> respository, IMapper mapper)
        {
            _repository = respository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<UserActivityHistoryDto>>> Get(CancellationToken cancellationToken)
        {
            var list = await _repository.TableNoTracking.ProjectTo<UserActivityHistoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Ok(list);
        }
        [HttpGet("{id:int}")]
        public async Task<ApiResult<UserActivityHistoryDto>> GetbyId(int id, CancellationToken cancellationToken)
        {
            var dto = await _repository.TableNoTracking.ProjectTo<UserActivityHistoryDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
            if (dto == null)
                return NotFound();
            return dto;
        }
        [HttpGet("{fullname}")]
        public async Task<ApiResult<UserActivityHistoryDto>> GetbyFullname(string fullname, CancellationToken cancellationToken)
        {
            var dto = await _repository.TableNoTracking.ProjectTo<UserActivityHistoryDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.FullName.Equals(fullname), cancellationToken);
            if (dto == null)
                return NotFound();
            return dto;
        }
    }
}

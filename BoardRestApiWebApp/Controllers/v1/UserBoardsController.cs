using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;
using Entities;
using RestApiProject.Models;
using Datas.Repositories;
using AutoMapper;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace RestApiProject.Controllers.v1
{
    [ApiVersion("1")]
    public class UserBoardsController : BaseController
    {
        private readonly IRepository<UserBoard> _repository;
        private readonly IMapper _mapper;
        public UserBoardsController(IRepository<UserBoard> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("{title}")]
        [AllowAnonymous]
        public async Task<ApiResult<List<UserBoardSelectDto>>> GetByTitle(string title, CancellationToken cancellationToken)
        {
            var list = await _repository.TableNoTracking.ProjectTo<UserBoardSelectDto>(_mapper.ConfigurationProvider)
                .Where(userboarddto => userboarddto.Title == title).ToListAsync(cancellationToken);
            if(list.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(list);
        }
        [HttpGet("{userBoardId:int}")]
        [AllowAnonymous]
        public async Task<ApiResult<UserBoardSelectDto>> GetById(int userBoardId, CancellationToken cancellationToken)
        {
            var userboarddto = await _repository.TableNoTracking.ProjectTo<UserBoardSelectDto>(_mapper.ConfigurationProvider)
                .Where(userboarddto => userboarddto.Id == userBoardId).SingleOrDefaultAsync(cancellationToken);
            if (userboarddto == null)
            {
                return NotFound();
            }
            return userboarddto;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<List<UserBoardDto>>> Get(CancellationToken cancellationToken)
        {
            var list = await _repository.TableNoTracking.ProjectTo<UserBoardDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return Ok(list);
        }
        [HttpPost]
        public virtual async Task<ApiResult<UserBoardDto>> Create(UserBoardDto dto, CancellationToken cancellationToken)
        {
            var model = dto.ToEntity(_mapper);
            await _repository.AddAsync(model, cancellationToken);
            var resultDto = await _repository.TableNoTracking.ProjectTo<UserBoardDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);
            return resultDto;
        }
        [HttpPut]
        public virtual async Task<ApiResult<UserBoardDto>> Update(int id, UserBoardDto dto, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, id);
            model = dto.ToEntity(_mapper, model);
            await _repository.UpdateAsync(model, cancellationToken);
            var resultDto = await _repository.TableNoTracking.ProjectTo<UserBoardDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);
            return resultDto;
        }

        [HttpDelete("{id}")]
        public virtual async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, id);
            await _repository.DeleteAsync(model, cancellationToken);
            return Ok();
        }
    }
}

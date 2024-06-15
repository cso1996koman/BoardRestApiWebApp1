using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;
using Entities;
using RestApiProject.Models;
using Datas.Repositories;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace RestApiProject.Controllers.v1
{
    [ApiVersion("1")]
    public class UserBoardImagesController : BaseController
    {
        private readonly IRepository<UserBoardImage> _repository;
        private readonly IMapper _mapper;
        public UserBoardImagesController(IRepository<UserBoardImage> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ApiResult<UserBoardImageDto>> GetByUserBoardId(int userBoardId, CancellationToken cancellationToken)
        {
            var list = await _repository.TableNoTracking.ProjectTo<UserBoardImage>(_mapper.ConfigurationProvider)
                .Where(userboarddto => userboarddto.UserBoardId == userBoardId).ToListAsync(cancellationToken);
            if (list.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(list);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<List<UserBoardImageDto>>> GetAll(CancellationToken cancellationToken)
        {
            var list = await _repository.TableNoTracking.ProjectTo<UserBoardImageDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return Ok(list);
        }
        [HttpPost]
        public virtual async Task<ApiResult<UserBoardImageDto>> Create(UserBoardImageDto dto, CancellationToken cancellationToken)
        {
            var model = dto.ToEntity(_mapper);
            await _repository.AddAsync(model, cancellationToken);
            var resultDto = await _repository.TableNoTracking.ProjectTo<UserBoardImageDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);
            return resultDto;
        }
        [HttpPut]
        public virtual async Task<ApiResult<UserBoardImageDto>> UpdatebyId(int id, UserBoardImageDto dto, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, id);
            model = dto.ToEntity(_mapper, model);
            await _repository.UpdateAsync(model, cancellationToken);
            var resultDto = await _repository.TableNoTracking.ProjectTo<UserBoardImageDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);
            return resultDto;
        }

        [HttpDelete("{id}")]
        public virtual async Task<ApiResult> DeletebyId(int id, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, id);
            await _repository.DeleteAsync(model, cancellationToken);
            return Ok();
        }
    }
}

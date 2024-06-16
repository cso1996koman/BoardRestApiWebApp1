using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;
using Entities;
using RestApiProject.Models;
using Datas.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace RestApiProject.Controllers.v1
{
    [ApiVersion("1")]
    public class UserBoardCommentsController : BaseController
    {
        private readonly IRepository<UserBoardComment> _repository;
        private readonly IMapper _mapper;
        public UserBoardCommentsController(IRepository<UserBoardComment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("{userBoardId:int}")]
        [AllowAnonymous]
        public async Task<ApiResult<UserBoardCommentDto>> GetByUserBoardId(int userBoardId, CancellationToken cancellationToken)
        {
            var list = await _repository.TableNoTracking.ProjectTo<UserBoardCommentDto>(_mapper.ConfigurationProvider)
                .Where(userboardcommentdto => userboardcommentdto.UserBoardId == userBoardId).ToListAsync(cancellationToken);
            if (list.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(list);
        }
        [HttpGet("{userboardtitle}")]
        [AllowAnonymous]
        public async Task<ApiResult<List<UserBoardCommentDto>>> GetByUserBoardTitle(string userboardtitle, CancellationToken cancellationToken)
        {
            var list = await _repository.TableNoTracking.ProjectTo<UserBoardCommentDto>(_mapper.ConfigurationProvider)
                .Where(userboardcommentdto => userboardcommentdto.UserBoard.Title.Equals(userboardtitle)).ToListAsync(cancellationToken);
            if (list.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(list);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<List<UserBoardCommentDto>>> GetAll(CancellationToken cancellationToken)
        {
            var list = await _repository.TableNoTracking.ProjectTo<UserBoardCommentDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return Ok(list);
        }
        [HttpPost]
        public virtual async Task<ApiResult<UserBoardCommentDto>> Create(UserBoardCommentDto dto, CancellationToken cancellationToken)
        {
            var model = dto.ToEntity(_mapper);
            await _repository.AddAsync(model, cancellationToken);
            var resultDto = await _repository.TableNoTracking.ProjectTo<UserBoardCommentDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);
            return resultDto;
        }
        [HttpPut]
        public virtual async Task<ApiResult<UserBoardCommentDto>> UpdatebyId(int id, UserBoardCommentDto dto, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, id);
            model = dto.ToEntity(_mapper, model);
            await _repository.UpdateAsync(model, cancellationToken);
            var resultDto = await _repository.TableNoTracking.ProjectTo<UserBoardCommentDto>(_mapper.ConfigurationProvider)
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

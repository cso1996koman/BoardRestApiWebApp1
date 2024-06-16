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
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace RestApiProject.Controllers.v1
{
    [ApiVersion("1")]
    public class UserBoardTopicsController : BaseController
    {
        private readonly IRepository<UserBoardTopic> _repository;
        private readonly IMapper _mapper;
        public UserBoardTopicsController(IRepository<UserBoardTopic> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("{userBoardId:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<UserBoardTopicDto>> GetUserBoardTopicByUserBoardId(int userBoardId, CancellationToken cancellationToken)
        {
            var list = await _repository.TableNoTracking.ProjectTo<UserBoardTopicDto>(_mapper.ConfigurationProvider)
                .Where(userboardtopicdto => userboardtopicdto.UserBoardId == userBoardId).ToListAsync(cancellationToken);
            if (list.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(list);
        }
        [HttpGet("{topic}")]
        [AllowAnonymous]
        public async Task<ActionResult<UserBoardTopicDto>> GetUserBoardTopicByTopic(string topic, CancellationToken cancellationToken)
        {
            var list = await _repository.TableNoTracking.ProjectTo<UserBoardTopicDto>(_mapper.ConfigurationProvider)
                .Where(userboardtopicdto => userboardtopicdto.Topic.Title.Equals(topic)).ToListAsync(cancellationToken);
            if (list.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(list);
        }
        [HttpPost]
        public virtual async Task<ApiResult<UserBoardTopicCreateDto>> Create(UserBoardTopicCreateDto dto, CancellationToken cancellationToken)
        {
            if(_repository.TableNoTracking.Where(userboardtopic => (userboardtopic.UserBoardId == dto.UserBoardId) && (userboardtopic.TopicId == dto.TopicId)).Count() > 0)
            {
                return BadRequest("이미 토픽을 가진 주제");
            }
            var model = dto.ToEntity(_mapper);
            await _repository.AddAsync(model, cancellationToken);
            var resultDto = await _repository.TableNoTracking.ProjectTo<UserBoardTopicCreateDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);
            return resultDto;
        }
        [HttpPut]
        public virtual async Task<ApiResult<UserBoardTopicDto>> UpdatebyId(int id, UserBoardTopicDto dto, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, id);
            model = dto.ToEntity(_mapper, model);
            await _repository.UpdateAsync(model, cancellationToken);
            var resultDto = await _repository.TableNoTracking.ProjectTo<UserBoardTopicDto>(_mapper.ConfigurationProvider)
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

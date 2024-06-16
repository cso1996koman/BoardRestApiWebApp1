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
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
namespace RestApiProject.Controllers.v1
{
    [ApiVersion("1")]
    public class TopicsController : BaseController
    {
        private readonly IRepository<Topic> _repository;
        private readonly IMapper _mapper;
        public TopicsController(IRepository<Topic> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("{parentTopicId:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<TopicDto>> GetByParentTopicId(int parentTopicId, CancellationToken cancellationToken)
        {
            var list = await _repository.TableNoTracking.ProjectTo<TopicDto>(_mapper.ConfigurationProvider)
                .Where(topicdto => topicdto.ParentTopicId == parentTopicId).ToListAsync(cancellationToken);
            if (list.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(list);
        }
        [HttpGet("{title}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<HavingUserBoardbyTopicDto>>> GetHavingUserBoardsBytitle(string title, CancellationToken cancellationToken)
        {
            var list = await _repository.TableNoTracking.ProjectTo<HavingUserBoardbyTopicDto>(_mapper.ConfigurationProvider)
                .Where(topicdto => topicdto.Title== title).ToListAsync(cancellationToken);
            if (list.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(list);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<List<TopicDto>>> GetAll(CancellationToken cancellationToken)
        {
            var list = await _repository.TableNoTracking.ProjectTo<TopicDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return Ok(list);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public virtual async Task<ApiResult<TopicDto>> Create(TopicDto dto, CancellationToken cancellationToken)
        {
            if (_repository.TableNoTracking.Where(topic => topic.Title.Equals(dto.Title)).Count() > 0)
            {
                return BadRequest("중복 주제");
            }
            var model = dto.ToEntity(_mapper);
            await _repository.AddAsync(model, cancellationToken);
            var resultDto = await _repository.TableNoTracking.ProjectTo<TopicDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);
            return resultDto;
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public virtual async Task<ApiResult<TopicDto>> UpdatebyTopicId(int topicId, TopicDto dto, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, topicId);
            model = dto.ToEntity(_mapper, model);
            await _repository.UpdateAsync(model, cancellationToken);
            var resultDto = await _repository.TableNoTracking.ProjectTo<TopicDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id.Equals(model.Id), cancellationToken);
            return resultDto;
        }

        [HttpDelete("{topicId:int}")]
        [Authorize(Roles = "Admin")]
        public virtual async Task<ApiResult> DeletebyTopicId(int topicId, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, topicId);
            await _repository.DeleteAsync(model, cancellationToken);
            return Ok();
        }

    }
}

using Datas.Repositories;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Services.DataInitializer
{
    public class DataInitializer : IDataInitializer
    {
        private readonly UserManager<User> _user_manager;
        private readonly RoleManager<Role> _role_manager;
        private readonly IRepository<Topic> _topic_repository;
        private readonly IRepository<UserBoard> _userboard_repository;
        private readonly IRepository<UserBoardTopic> _userboardtopic_repository;
        public DataInitializer(UserManager<User> UserManager, RoleManager<Role> RoleManager, IRepository<Topic> TopicRepository
            , IRepository<UserBoard> UserBoardRepository, IRepository<UserBoardTopic> UserBoardTopicRepository)
        {
            _user_manager = UserManager;
            _role_manager = RoleManager;
            _topic_repository = TopicRepository;
            _userboard_repository = UserBoardRepository;
            _userboardtopic_repository = UserBoardTopicRepository;
        }
        public void InitializeData()
        {
            if (!_role_manager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
            {
                _role_manager.CreateAsync(new Role { Name = "Admin", Description = "Admin role" }).GetAwaiter().GetResult();
            }
            if (!_user_manager.Users.AsNoTracking().Any(p => p.UserName == "adminsample1"))
            {
                var user = new User
                {
                    Age = 25,
                    FullName = "adminsample1",
                    Gender = GenderType.Male,
                    UserName = "adminsample1",
                    Email = "cso_199_k@gmail.com"
                };
                _user_manager.CreateAsync(user, "123456").GetAwaiter().GetResult();
                _user_manager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
            }
            if (!_user_manager.Users.AsNoTracking().Any(p => p.UserName == "adminsample2"))
            {
                var user = new User
                {
                    Age = 25,
                    FullName = "adminsample2",
                    Gender = GenderType.Female,
                    UserName = "adminsample2",
                    Email = "sco_199_k@gmail.com"
                };
                _user_manager.CreateAsync(user, "123456").GetAwaiter().GetResult();
                _user_manager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
            }

            if (!_topic_repository.TableNoTracking.Any(p => p.Title == "중고판매"))
            {
                _topic_repository.Add(new Topic
                {
                    AuthorId = 1,
                    Title = "중고판매"

                });

            }
            if (!_topic_repository.TableNoTracking.Any(p => p.Title == "신발의류"))
            {
                _topic_repository.Add(new Topic
                {
                    AuthorId = 1,
                    Title = "신발의류",
                    ParentTopicId = 1
                });

            }
            if (!_topic_repository.TableNoTracking.Any(p => p.Title == "전자제품"))
            {
                _topic_repository.Add(new Topic
                {
                    AuthorId = 1,
                    Title = "전자제품",
                    ParentTopicId = 1
                });


            }
            if (!_topic_repository.TableNoTracking.Any(p => p.Title == "식자재"))
            {
                _topic_repository.Add(new Topic
                {
                    AuthorId = 1,
                    Title = "식자재",
                    ParentTopicId = 1
                });

            }

            if (!_userboard_repository.TableNoTracking.Any(p => p.Id == 1))
            {
                _userboard_repository.Add(new UserBoard
                {
                    Title = "나이키 신발 팔아요",
                    Content = "작년에 샀지만 몇번 안신어서 거의 새거랑 같은 나이키 제품 신발 팝니다. 거래는 서울 여의도역 근처에서 합니다. 연락주세요.",
                    AuthorId = 1,
                    CreateDate = new System.DateTimeOffset()
                });
            }
            if (!_userboard_repository.TableNoTracking.Any(p => p.Id == 2))
            {
                _userboard_repository.Add(new UserBoard
                {
                    Title = "레노버 노트북 중고제품 팝니다.",
                    Content = "레노버 노트북 중고제품 팔아요. 구매한지 2년지났지만, 그래픽카드 메모리 SSD 교체해서 완전 잘돌아갑니다. !!",
                    AuthorId = 1,
                    CreateDate = new System.DateTimeOffset()
                });
            }
            if (!_userboard_repository.TableNoTracking.Any(p => p.Id == 3))
            {
                _userboard_repository.Add(new UserBoard
                {
                    Title = "직접 기른 감자팔아요.",
                    Content = "직접 재배해서 농약도 안치고 아주 신선합니다. 직송으로 배송해드리고 5KG 이상 구매시, 배송비는 무료로 해드립니다. ",
                    AuthorId = 1,
                    CreateDate = new System.DateTimeOffset()
                });
            }

            if (!_userboardtopic_repository.TableNoTracking.Any(p => p.Id == 1))
            {
                _userboardtopic_repository.Add(new UserBoardTopic
                {
                    TopicId = 2,
                    UserBoardId = 1
                });
            }
            if (!_userboardtopic_repository.TableNoTracking.Any(p => p.Id == 2))
            {
                _userboardtopic_repository.Add(new UserBoardTopic
                {
                    TopicId = 3,
                    UserBoardId = 2
                });
            }
            if (!_userboardtopic_repository.TableNoTracking.Any(p => p.Id == 3))
            {
                _userboardtopic_repository.Add(new UserBoardTopic
                {
                    TopicId = 4,
                    UserBoardId = 3
                });
            }

        }
    }
}

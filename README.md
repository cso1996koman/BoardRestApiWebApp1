# 상세 스펙
## ASP .NET Core identity 모듈을 활용한 사용자 로그인 관리 
   * 사용자 계층 관리 (관리자, 일반이용자)
   * 리소스 재접근시 보안스탬프 재발행 및 로그인 만료시간 복귀 -> 보안강화 및 사용자 용이성증가
## JWT발생을 통한 리소스 접근권한부여
   * 암호화된 토큰을 통한 리소스접근 -> 보안강화
## 오류 및 예외 및 이벤트 로깅
   * Elmah모듈을 이용하여 로그데이터 xml파일 저장 및 MySql 데이터베이스에 저장
   * NLog을 이용하여 txt 파일 및 콘솔에 오류 및 예외 기록
## 의존성 주입
   * ASP .NET CORE IOC 컨테이너 활용하여 서비스 등록
   * AutoFac 라이브러리와 Reflection 활용하여 인터페이스 등록시 구현 클래스 객체 자동 생성
      - entity 객체 자동 생성
      - custome 서비스 자동 생성
## 데이터베이스 관리
   * Entity Framework Core 모듈 활용하여 데이터 (조회, 수정, 삭제, 생성)
   * Migration 모듈을 통한 테이블, 제약조건, 키 (생성, 수정, 삭제)
   * Entity, Model 간에 변수네이밍을 통한 매핑  
## 컨트롤러 모듈 로직
   * <Dto , Entity> 간에 자동 매핑을 통해 Dto 데이터 캡슐화
## 모듈 설계
   * Asp .Net Core Mvc를 활용하여 Model, Controller, View(Swagger)로 분할
## Swagger을 통한 Rest Api 테스팅 및 문서화
   * Http 버전 관리
   * JWT, OAuth 기반 인증 적용
   * Api 기능 요약
   * 입력 출력 데이터 예시
   * 예외 결과 호출스택 로킹
# 테스트 시나리오 설명
## 1. 비로그인상태 기능
    User All Get을 제외한 모든 Scheme에 대한 Get기능(by[...Id] 포함)
## 2. 로그인상태 기능
  ### 2.1 관리자(Admin)
     - 모든기능 가능
  ### 2.2 일반 이용자
     - Topic (Post, Put, Delete) 기능, User (All Get) 을 제외한 모든 Scheme에 대한 기능
## 3. 예외 시나리오
  ### 3.1. 예외 상황
     - 비로그인상태 모든 Scheme에 대한 Post, Put, Delete 기능 사용
     - 관리자외에 User All Get, Delete 기능 사용
     - 관리자외에 Topic Post, Put, Delete 기능 사용
     - 비타당한 입력값을 가진 요청
         * NotNull Input에 대해 Null값 (ex. UserBoard Put시 Content Null값 입력)
         * 외래키 충돌 (ex. 존재하지 않는 UserID 및 TopicId를 입력한 UserBoard Post 요청) 등
         * 중복 데이터 (ex. 같은 title 가진 주제 생성, 같은 fullname 가진 사용자 생성, 게시글에 대한 같은 주제 생성)
 ### 3.2. 예외 결과
    - 비권한 접근 예외
    - 외래키 충돌 예외
    - Not Found 예외
    - 무응답
    - Bad Request 예외 (중복 데이터)
## 4. 초기 데이터값
  - UserData (Id(fullname) : adminsample1, password : 123456) - Admin role을 가진 계정
  - Topic [(title : 중고판매), (title : 신발의류), (title : 전자제품), (title : 식자재)]
  - ...
## 5. 테스트 시나리오 [모든 id는 1부터 순차적으로 할당(계정 로그인시 사용되는 id는 fullname에 해당)] 
  ### 5.1. 게시글 등록
    1. 사용자 계정 생성(User Post) 혹은 기존 계정 로그인(3.으로)
    2. 생성한 계정으로 로그인(Authorize)
    3. NotNull입력값 모두 입력이후 UserBoard Post(NotNullInput : title, content, authorId(userId)) [Get /api/v1/Users/Fullname 요청을 통해 id(userId) 확인]
    4. Get /api/v1/UserBoards/title을 이용하여 입력한 게시글 확인
  ### 5.2. 게시글 수정
    1. 사용자 계정 생성(User Post) 혹은 기존 계정 로그인(3.으로)
    2. 생성한 계정으로 로그인(Authorize) 
    3. NotNull입력값 모두 입력이후 UserBoard Put(NotNullInput : title, content, authorId(userId)) [Get /api/v1/Users/Fullname 요청을 통해 id(userId) 확인]
    4. Get /api/v1/UserBoards/title 을 이용하여 수정한 게시글 확인
  ### 5.3. 게시글 삭제
    1. 사용자 계정 생성(User Post) 혹은 기존 계정 로그인(3.으로) 
    2. 생성한 계정으로 로그인(Authorize) 
    3. UserBoardId값 입력이후 UserBoard Delete 
    4. Get /api/v1/UserBoards/title 을 이용하여 삭제 확인 -> NotFound
  ### 5.2. 주제 등록 
    1. User에 기존 데이터인 Admin으로 Authorize 로그인 [초기 데이터 Admin 참조]
    2. NotNull입력값 모두 입력이후 Topic Post(NotNullInput : title, authorId(userId)) [Get /api/v1/Users/Fullname 요청을 통해 id(userId) 확인]
    3. Topic Scheme에 Get /api/v1/Topics/title 이용하여 입력한 토픽 확인
  ### 5.3. 게시글 댓글 등록
    1. 사용자 계정 생성(User Create) 혹은 기존 계정 로그인(3.으로) 
    2. 생성한 계정을 로그인(Authorize) 
    3. NotNull입력값 모두 입력이후 UserBoardComment Post(NotNullInput : content, userboardId, authorId(userId)) [Get /api/v1/UserBoards/title 요청을 통해 id(userboardId) 확인] [Get /api/v1/Users/Fullname 요청을 통해 id(userId) 확인] 
    4. UserBoardComment Schem에 Get /api/v1/byUserboardTitle 을 이용하여 게시글에 등록한 댓글확인
  ### 5.4. 게시글 주제 등록
    1. 사용자 계정 생성(User Create) 혹은 기존 계정 로그인(3.으로) 
    2. 생성한 계정을 로그인(Authorize) 
    3. NotNull입력값 모두 입력이후 UserBoardTopic Scheme에 Post(NotNullInput : topicId, userBoardId) [Get /api/v1/Topics/title 요청을 통해 id(topicId) 확인] [Get /api/v1/UserBoards/title 요청을 통해 id(userBoardId) 확인]
    4. UserBoardTopic Schem에 Get /api/v1/UserBoardTopics/userBoardId을 이용하여 게시글에 등록한 주제 확인


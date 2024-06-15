using System.ComponentModel.DataAnnotations;

namespace Common
{
    public enum ApiResultStatusCode
    {
        [Display(Name = "작업이 성공적으로 완료되었습니다.")]
        Success = 0,

        [Display(Name = "서버에서 오류가 발생했습니다.")]
        ServerError = 1,

        [Display(Name = "전달된 매개변수가 유효하지 않습니다.")]
        BadRequest = 2,

        [Display(Name = "찾을수 없음")]
        NotFound = 3,

        [Display(Name = "목록이 비어있습니다.")]
        ListEmpty = 4,

        [Display(Name = "처리오류가 발생했습니다.")]
        LogicError = 5,

        [Display(Name = "인증오류")]
        UnAuthorized = 6
    }
}

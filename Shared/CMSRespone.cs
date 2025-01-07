using System.Collections.Generic;

namespace Shared
{


    public class CMSResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string BuildQuery { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        public StatusCode StatusCode { get; set; }

        public CMSResponse()
        {
            Success = true;
            Errors = new List<string>();
        }

        public static CMSResponse<T> SuccessResult(T data, string message = "Thao tác thành công!", string buildQuery = "")
        {
            return new CMSResponse<T>
            {
                Success = true,
                Message = message,
                BuildQuery = buildQuery,
                Data = data,
                StatusCode = StatusCode.ThanhCong
            };
        }
       

        public static CMSResponse<T> ErrorResult(string message, StatusCode statusCode = StatusCode.YeuCauKhongHopLe)
        {
            return new CMSResponse<T>
            {
                Success = false,
                Message = message,
                StatusCode = statusCode,
                Errors = new List<string> { message }
            };
        }

        public void AddError(string error)
        {
            Success = false;
            Errors.Add(error);
        }
    }
}

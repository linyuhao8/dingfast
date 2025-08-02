namespace Api.Commons;

public class ApiResponse<T>
/// <summary>
/// 期待所有API回傳格式為
// {
//   "success": false,
//   "data": null,
//   "Message": "Conversation not found",
//   "errorCode": 40401, 可以在Common/ErrorCode.cs裡面找到對應狀態
//   "errorDetail": "資料庫內找不到對話"
// }
/// </summary>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public int? ErrorCode { get; set; }
    public string? ErrorDetail { get; set; }

    // 成功回傳，新增可選 message 參數
    public static ApiResponse<T> Ok(T data, string? message = null) =>
        new() { Success = true, Data = data, Message = message };

    // 失敗回傳，message 可為 nullable
    public static ApiResponse<T> Fail(string? message, int? code = null, string? detail = null) =>
        new() { Success = false, Message = message, ErrorCode = code, ErrorDetail = detail };
}
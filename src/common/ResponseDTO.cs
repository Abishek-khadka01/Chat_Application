namespace Chat_Application.src.common
{

    public record SuccessFulResponse<TData>(bool Success, string Message, TData Data);


    public record ErrorResponse ( string ErrorMessage, bool Success = false);
}